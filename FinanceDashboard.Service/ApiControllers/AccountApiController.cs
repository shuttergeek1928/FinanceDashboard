using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
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
        /// Returns all users currently in the system
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
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
