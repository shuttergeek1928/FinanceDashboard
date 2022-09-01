using AutoMapper;
using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/[controller]")]
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
                IEnumerable<UserModel> villaList = _mapper.Map<List<UserModel>>(await _userDataController.GetAllAsync());
                
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
    }
}
