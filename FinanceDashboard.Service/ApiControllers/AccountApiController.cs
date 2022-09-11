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

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllUsers1()
        {
            return Ok(await _ac.GetAllUsers());
        }

        [HttpPost]
        [Route("create-dummy-user")]
        public async Task<ActionResult<ApiResponse>> CreateDummyUser()
        {
            return Ok(await _ac.CreateDummyUser());
        }
    }
}
