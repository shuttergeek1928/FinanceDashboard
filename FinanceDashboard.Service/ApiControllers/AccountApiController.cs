using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Core.Security;
using FinanceDashboard.Models.PreLogon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = FinanceDashboard.Core.Security.AuthorizeAttribute;

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
        /// Call this api to logout
        /// </summary>
        /// <param name="model"></param>
        /// <returns>null</returns>
        //[HttpPost]
        //[Route("logout")]
        //public async Task<ActionResult<ApiResponse>> SignOut()
        //{
        //    return Ok( _ac.SignOut());
        //}

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
        [HttpGet]
        [Authorize]
        [Route("all")]
        public async Task<ActionResult<ApiResponse>> GetAllUsers()
        {
            return Ok(await _ac.GetAllUsers());
        }

        /// <summary>
        /// Search user by email
        /// </summary>
        /// <returns>Details of sers</returns>
        [HttpGet]
        [Route("{email}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetUserByEmail(string email)
        {
            return Ok(await _ac.GetUserByEmail(email));
        }

        /// <summary>
        /// Search user by account id
        /// </summary>
        /// <returns>Details of account</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetUserByAccountId(int id)
        {
            return Ok(await _ac.GetUserByAccountId(id));
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
