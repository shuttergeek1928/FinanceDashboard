using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Models.Models.Income;
using FinanceDashboard.Models.Models.SegmentLimits;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = FinanceDashboard.Core.Security.AuthorizeAttribute;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/limits/"), Authorize]
    [ApiController]
    public class SegmentLimitsApiController : ControllerBase
    {
        private readonly SegmentLimitsController _sc;

        public SegmentLimitsApiController(SegmentLimitsController sc)
        {
            _sc = sc;
        }

        /// <summary>
        /// Returns all income for all users.
        /// </summary>
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this limit to restrict number of record returned</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<IncomeListModel>>> GetIncomes(int? limit = 1000, string? includeChildProperty = null)
        {
            return Ok(await _sc.GetSegmnetLimitDetails(limit, includeChildProperty));
        }
        
        /// <summary
        /// Returns all income for loggeg in user.
        /// </summary>
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this limit to restrict number of record returned</param>
        /// <returns></returns>
        [HttpGet]
        [Route("accountId")]
        public async Task<ActionResult<ApiResponse>> GetIncomesByAccountId(int? accountId, int? limit = 1000, string? includeChildProperty = null)
        {
            return Ok(await _sc.GetSegmnetLimitDetailsByAccountId(accountId, limit, includeChildProperty));
        }

        /// <summary>
        /// Creates new income for current logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ApiResponse>> CreateIncome(SegmentLimitsCreateModel model)
        {
            return Ok(await _sc.CreateLimits(model));
        }

        /// <summary>
        /// Returns all subscription for current logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create-dummy-limit")]
        public async Task<ActionResult<ApiResponse>> CreateDummyLimits()
        {
            return Ok(await _sc.CreateDummyLimits());
        }
    }
}