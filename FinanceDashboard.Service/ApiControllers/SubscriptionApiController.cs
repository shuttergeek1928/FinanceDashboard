using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Models.Subscription;
using FinanceDashboard.Data.DataController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/subscription/")]
    [ApiController, Authorize]
    public class SubscriptionApiController : ControllerBase
    {
        private readonly SubscriptionController _sc;
        private readonly AccountDataController _adc;

        public SubscriptionApiController(SubscriptionController sc)
        {
            _sc = sc;
        }

        /// <summary>
        /// Returns all subscription irrespective of account.
        /// </summary>
        /// <param name="includeChildProperty"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscription(string? includeChildProperty = null)
        {
            return Ok(await _sc.GetAllSubscription(includeChildProperty));
        }

        /// <summary>
        /// Search subscriptions linked to an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="includeChildProperty"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all/accountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscriptionByAccountId(int? accountId, string? includeChildProperty = null)
        {
            return Ok(await _sc.GetAllSubscriptionByAccountId(accountId, includeChildProperty));
        }

        /// <summary>
        /// Search subscription by subscription id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeChildProperty"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> GetSubscriptionById(Guid id, string? includeChildProperty = null)
        {
            return Ok(await _sc.GetSubscriptionById(id, includeChildProperty));
        }

        /// <summary>
        /// Update an existing subscription
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> UpdateSubscription(SubscriptionUpdateModel model, Guid id)
        {
            return Ok(await _sc.UpdateSubscription(model, id));
        }

        /// <summary>
        /// Create new subscription for an account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly create subscription for an account</returns>
        [HttpPost]
        [Route("create/new")]
        public async Task<ActionResult<ApiResponse>> CreateNewSubscription(SubscriptionCreateModel model)
        {
            return Ok(await _sc.CreateNewSubscription(model));
        }

        /// <summary>
        /// Delete the subscription by subscription id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> DeleteSubscriptionById(Guid id)
        {
            return Ok(await _sc.DeleteSubscriptionById(id));
        }

        /// <summary>
        /// Get the total subscription amount for an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("totalSubscriptionValue/byAccountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int? accountId)
        {
            return Ok(await _sc.GetTotalAmount(accountId));
        }

        /// <summary>
        /// Get all active or expired subscription on an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="isExpired"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("allActiveOrExpiredSubscriptions/byAccountId")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int accountId, bool? isExpired = false)
        {
            return Ok(await _sc.GetActiveOrExpiredSubscription(accountId, isExpired));
        }

        /// <summary>
        /// Mark a subscription as expired
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("expire/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> ExpireSubscriptionsById(Guid id)
        {
            return Ok(await _sc.ExpireSubscriptionsById(id));
        }

        /// <summary>
        /// Create dummy subscription
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create-dummy-subscription")]
        public async Task<ActionResult<ApiResponse>> CreateDummySubscription()
        {
            return Ok(await _sc.CreateDummySubscription());
        }
    }
}