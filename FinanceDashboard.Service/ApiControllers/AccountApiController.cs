using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Models.PreLogon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/account/")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly AccountController _ac;

        public AccountApiController(AccountController ac)
        {
            _ac = ac;
        }

        /// <summary>
        /// Call this api to login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT token for logged in user</returns>
        [HttpPost]
        [Route("logon")]
        public async Task<ActionResult<ApiResponse>> Logon(AccountLogonModel model)
        {
            return Ok(await _ac.LogonAccount(model));
        }
        /// <summary>
        /// Register for new user
        /// </summary>
        /// <returns>Newly created user.</returns>
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ApiResponse>> RegisterAccount(RegisterAccountModel model)
        {
            return Ok(await _ac.RegisterAccount(model));
        }

        /// <summary>
        /// Returns all users currently in the system
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet, Authorize]
        public async Task<ActionResult<ApiResponse>> GetAllUsers1()
        {
            return Ok(await _ac.GetAllUsers());
        }

        /// <summary>
        /// Create a new dummy user for testing POST api
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create-dummy-user")]
        public async Task<ActionResult<ApiResponse>> CreateDummyUser()
        {
            return Ok(await _ac.CreateDummyUser());
        }
    }
}
