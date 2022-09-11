using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Models.Subscription;
using FinanceDashboard.Data.DataController;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/subscription/")]
    [ApiController]
    public class SubscriptionApiController : ControllerBase
    {
        private readonly SubscriptionController _sc;
        private readonly AccountDataController _adc;

        public SubscriptionApiController(SubscriptionController sc)
        {
            _sc = sc;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscription(string? includeChildProperty = null)
        {
            return Ok(await _sc.GetAllSubscription(includeChildProperty));
        }

        [HttpGet]
        [Route("all/accountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscriptionByAccountId(int? accountId, string? includeChildProperty = null)
        {
            return Ok(await _sc.GetAllSubscriptionByAccountId(accountId, includeChildProperty));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> GetSubscriptionById(Guid id, string? includeChildProperty = null)
        {
            return Ok(await _sc.GetSubscriptionById(id, includeChildProperty));
        }

        [HttpPut]
        [Route("update/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> UpdateSubscription(SubscriptionUpdateModel model, Guid id)
        {
            return Ok(await _sc.UpdateSubscription(model, id));
        }

        [HttpPost]
        [Route("create/new")]
        public async Task<ActionResult<ApiResponse>> CreateNewSubscription(SubscriptionCreateModel model)
        {
            return Ok(await _sc.CreateNewSubscription(model));
        }

        [HttpDelete]
        [Route("delete/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> DeleteSubscriptionById(Guid id)
        {
            return Ok(await _sc.DeleteSubscriptionById(id));
        }

        [HttpGet]
        [Route("totalSubscriptionValue/byAccountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int? accountId)
        {
            return Ok(await _sc.GetTotalAmount(accountId));
        }

        [HttpGet]
        [Route("allActiveOrExpiredSubscriptions/byAccountId")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int accountId, bool? isExpired = false)
        {
            return Ok(await _sc.GetActiveOrExpiredSubscription(accountId, isExpired));
        }

        [HttpPatch]
        [Route("expire/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> ExpireSubscriptionsById(Guid id)
        {
            return Ok(await _sc.ExpireSubscriptionsById(id));
        }

        [HttpPost]
        [Route("create-dummy-subscription")]
        public async Task<ActionResult<ApiResponse>> CreateDummySubscription()
        {
            return Ok(await _sc.CreateDummySubscription());
        }
    }
}