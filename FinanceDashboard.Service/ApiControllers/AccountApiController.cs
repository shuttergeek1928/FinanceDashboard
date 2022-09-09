using AutoMapper;
using FinanceDashboard.Data.SqlServer.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly AccountDataController _adc;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;

        public AccountApiController(AccountDataController adc, IMapper mapper)
        {
            _adc = adc;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllUsers()
        {
            try
            {
                IEnumerable<AccountListModel> accounts = _mapper.Map<List<AccountListModel>>(await _adc.GetAllAsync());
                
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
                _response.Result = await _adc.CreateUserAsync(_mapper.Map<Account>(user));

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
