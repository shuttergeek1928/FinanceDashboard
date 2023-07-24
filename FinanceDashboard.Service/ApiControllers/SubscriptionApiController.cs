using FinanceDashboard.Core;
using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Models.Subscription;
using FinanceDashboard.Data.DataController;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = FinanceDashboard.Core.Security.AuthorizeAttribute;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/subscription/"), Authorize]
    [ApiController]
    public class SubscriptionApiController : ControllerBase
    {
        private readonly SubscriptionController _sc;

        public SubscriptionApiController(SubscriptionController sc)
        {
            _sc = sc;
        }

        /// <summary>
        /// Returns all subscription for current logged in user.
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
        [Route("all/accountId")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscriptionByAccountId(int? accountId = null, string? includeChildProperty = null)
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
        /// <param name="checkFirst">Use this bool flag to check new total subsciption value before creting, if false create anyways else mark it as true</param>
        /// <returns>A newly create subscription for an account</returns>
        [HttpPost]
        [Route("create/new")]
        public async Task<ActionResult<ApiResponse>> CreateNewSubscription(SubscriptionCreateModel model, bool checkFirst = true)
        {
            return Ok(await _sc.CreateNewSubscription(model, checkFirst));
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
        /// Get the total subscription amount for an current user
        /// </summary>
        /// <returns>Total amount of subscriptions for current logged in user.</returns>
        [HttpGet]
        [Route("totalSubscriptionValue")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmountOfSubscription()
        {
            return Ok(await _sc.GetTotalAmount());
        }

        /// <summary>
        /// Get the total subscription amount for an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("totalSubscriptionValue/accountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int? accountId)
        {
            return Ok(await _sc.GetTotalAmount(accountId));
        }

        /// <summary>
        /// Get all active or expired subscription for current user
        /// </summary>
        /// <param name="isExpired"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("allActiveOrExpiredSubscriptions")]
        public async Task<ActionResult<ApiResponse>> GetAllActiveOrExpiredSubscriptions(bool? isExpired = false)
        {
            return Ok(await _sc.GetActiveOrExpiredSubscription(isExpired));
        }
        
        /// <summary>
        /// Get all active or expired subscription on an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="isExpired"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("allActiveOrExpiredSubscriptions/accountId")]
        public async Task<ActionResult<ApiResponse>> GetAllActiveOrExpiredSubscriptions(int accountId, bool? isExpired = false)
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