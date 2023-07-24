using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Models.Models.Income;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = FinanceDashboard.Core.Security.AuthorizeAttribute;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/income/"), Authorize]
    [ApiController]
    public class IncomeApiController : ControllerBase
    {
        private readonly IncomeController _ic;

        public IncomeApiController(IncomeController ic)
        {
            _ic = ic;
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
            return Ok(await _ic.GetIncomeDetails(limit, includeChildProperty));
        }
        
        /// <summary>
        /// Returns all income for loggeg in user.
        /// </summary>
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this limit to restrict number of record returned</param>
        /// <returns></returns>
        [HttpGet]
        [Route("accountId")]
        public async Task<ActionResult<ApiResponse>> GetIncomesByAccountId(int? accountId, int? limit = 1000, string? includeChildProperty = null)
        {
            return Ok(await _ic.GetIncomesByAccountId(accountId, limit, includeChildProperty));
        }

        /// <summary>
        /// Creates new income for current logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ApiResponse>> CreateIncome(IncomeCreateModel model)
        {
            return Ok(await _ic.CreateIncome(model));
        }

        /// <summary>
        /// Use this api to update an existing income details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("update/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateIncome(IncomeUpdateModel model, Guid id)
        {
            return Ok(await _ic.UpdateIncome(model, id));
        }

        [HttpGet]
        [Route("getBalance")]
        public async Task<ActionResult> GetBalacne()
        {
            return Ok(await _ic.GetBalance());
        }

        /// <summary>
        /// Returns all subscription for current logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create-dummy-income")]
        public async Task<ActionResult<ApiResponse>> CreateDummyIncome()
        {
            return Ok(await _ic.CreateDummyIncome());
        }
    }
}