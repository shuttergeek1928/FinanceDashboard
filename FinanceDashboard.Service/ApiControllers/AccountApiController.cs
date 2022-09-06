using AutoMapper;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IAccountDataController _accountDataController;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;

        public AccountApiController(IAccountDataController accountDataController, IMapper mapper)
        {
            _accountDataController = accountDataController;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllUsers()
        {
            try
            {
                IEnumerable<AccountListModel> accounts = _mapper.Map<List<AccountListModel>>(await _accountDataController.GetAllAsync());
                
                _response.Result = accounts;

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Route("create-dummy-user")]
        public async Task<ActionResult<ApiResponse>> CreateDummyUser()
        {

            AccountDetailModel user = new AccountDetailModel
            {
                Id = Guid.NewGuid(),
                Name = "Dummy Dummy",
                FirstName = "Dummy",
                LastName = "Dummy",
                Email = DateTime.Now.ToString(),
                PasswordHash = "Dummy",
                HashingSalt = "Dummy",
                PasswordHashHistory = "Dummy",
                MobileNumber = "9999999999",
                CreatedOn = DateTime.Now
            };

            try
            {
                _response.Result = await _accountDataController.CreateUserAsync(_mapper.Map<AccountCreateModel>(user));

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }
    }
}
