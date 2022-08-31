using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Data.DataControllers;
using FinanceDashboard.Data.Entities.User;

namespace FinanceDashboard.Service.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TestUserController : ControllerBase
    {
        private Data.DataControllers.TestUserController _tu = new ();
        public TestUserController()
        {
            //_tu = tu;
        }

        [HttpGet]
        [Route("getUser")]
        public User ReadUser()
        {
            return _tu.ReadUser();
        }
    }
}
