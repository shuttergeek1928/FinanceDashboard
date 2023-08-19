using AuthorizeAttribute = FinanceDashboard.Core.Security.AuthorizeAttribute;
using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Core;
using FinanceDashboard.Models.Models.Income;
using FinanceDashboard.Models.Models.Emi;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/emi"), Authorize]
    [ApiController]
    public class EmiApiController : ControllerBase
    {
        private readonly EmiController _ec;

        public EmiApiController(EmiController ec)
        {
            _ec = ec;
        }

        /// <summary>
        /// Returns all Emi for all users.
        /// </summary>
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this limit to restrict number of record returned</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<EmiListModel>>> GetEmi(int? limit = 1000, string? includeChildProperty = null)
        {
            return Ok(await _ec.GetEmiDetails(limit, includeChildProperty));
        }

        /// <summary>
        /// Returns all Emi for loggeg in user.
        /// </summary>
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this limit to restrict number of record returned</param>
        /// <returns></returns>
        [HttpGet]
        [Route("accountId")]
        public async Task<ActionResult<ApiResponse>> GetEmiByAccountId(int? accountId, int? limit = 1000, string? includeChildProperty = null)
        {
            return Ok(await _ec.GetEmiByAccountId(accountId, limit, includeChildProperty));
        }

        /// <summary>
        /// Creates new Emi for current logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ApiResponse>> CreateEmi(EmiCreateModel model)
        {
            return Ok(await _ec.CreateEmi(model));
        }

        /// <summary>
        /// Use this api to update an existing Emi details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("update/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateEmi(EmiUpdateModel model, Guid id)
        {
            return Ok(await _ec.UpdateEmi(model, id));
        }

        /// <summary>
        /// Returns all subscription for current logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create-dummy-Emi")]
        public async Task<ActionResult<ApiResponse>> CreateDummyEmi()
        {
            return Ok(await _ec.CreateDummyEmi());
        }
    }
}
