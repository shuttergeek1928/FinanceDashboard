using AutoMapper;
using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly AccountController _ac;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;

        public TestApiController(AccountController ac, IMapper mapper)
        {
            _ac = ac;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Route("testCoreController")]
        public async Task<ActionResult> GetAccounts()
        {
            return Ok(await _ac.GetAllUsers());
        }


    }
}
