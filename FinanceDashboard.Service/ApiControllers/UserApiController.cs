using AutoMapper;
using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserDataController _userDataController;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;

        public UserApiController(IUserDataController userDataController, IMapper mapper)
        {
            _userDataController = userDataController;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllUsers()
        {
            try
            {
                IEnumerable<UserListModel> villaList = _mapper.Map<List<UserListModel>>(await _userDataController.GetAllAsync());
                
                _response.Result = villaList;

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

            UserDetailModel user = new UserDetailModel
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
                _response.Result = await _userDataController.CreateAsync(_mapper.Map<User>(user));

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
