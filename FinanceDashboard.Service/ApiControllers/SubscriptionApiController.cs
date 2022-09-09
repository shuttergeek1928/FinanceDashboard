using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Models.Subscription;
using FinanceDashboard.Data.SqlServer.DataController;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/subscription/")]
    [ApiController]
    public class SubscriptionApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly SubscriptionDataController _sdc;
        private readonly AccountDataController _adc;

        public SubscriptionApiController(AccountDataController adc, IMapper mapper, SubscriptionDataController sdc)
        {
            _adc = adc;
            _mapper = mapper;
            _response = new();
            _sdc = sdc;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscription(string? includeChildProperty = null)
        {
            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(includeChildProperties: includeChildProperty));

                _response.Result = subscriptions;

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Route("all/accountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscriptionByAccountId(int? accountId, string? includeChildProperty = null)
        {
            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(x => x.User.AccountId == accountId, includeChildProperties: includeChildProperty));

                _response.Result = subscriptions;

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> GetSubscriptionById(Guid id, string? includeChildProperty = null)
        {
            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(x => x.Id == id, includeChildProperties: includeChildProperty));

                _response.Result = subscriptions;

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPut]
        [Route("update/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> UpdateSubscription(SubscriptionUpdateModel model, Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Subscription subscription = _mapper.Map<Subscription>(await _sdc.GetAsync(x => x.Id == id));

            if (subscription == null)
                throw new Exception($"Subscription of id: {id} does not exist");

            try
            {
                subscription.LastUpdate = DateTime.Now;

                subscription.LastUpdateBy = subscription.AccountId;

                _response.Result = await _sdc.UpdateSubscription(_mapper.Map<SubscriptionUpdateModel, Subscription>(model, subscription));

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Route("create/new")]
        public async Task<ActionResult<ApiResponse>> CreateNewSubscription(SubscriptionCreateModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _adc.GetAsync(x => x.AccountId == model.AccountId);

            if (account == null)
                throw new Exception($"Account id: {model.AccountId} does not exist");

            try
            {
                _response.Result = await _sdc.CreateAsync(_mapper.Map<Subscription>(model));

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpDelete]
        [Route("delete/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> DeleteSubscriptionById(Guid id)
        {
            Subscription subscription = _mapper.Map<Subscription>(await _sdc.GetAsync(x => x.Id == id));

            if (subscription == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Result = $"Subscription of id: {id} not found";
                return _response;
            }

            try
            {
                await _sdc.RemoveAsync(subscription);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = $"Subscription of id: {id} deleted";
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Route("totalSubscriptionValue/byAccountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int? accountId)
        {
            var account = await _adc.GetAsync(x => x.AccountId == accountId);

            if (account == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Result = $"Account id: {accountId} does not exist";
                return _response;
            }

            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(x => x.User.AccountId == accountId));

                _response.Result = CalculateTotalAmount(subscriptions);

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Route("allActiveOrExpiredSubscriptions/byAccountId")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int accountId, bool? isExpired = false)
        {
            var account = await _adc.GetAsync(x => x.AccountId == accountId);

            if (account == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Result = $"Account id: {accountId} does not exist";
                return _response;
            }

            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>
                    (
                        await _sdc.GetAllAsync(x => 
                        x.User.AccountId == accountId &&
                        x.IsExpired == isExpired)
                    );

                _response.Result = subscriptions;

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPatch]
        [Route("expire/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> ExpireSubscriptionsById(Guid id)
        {
            JsonPatchDocument<Subscription> sub = new JsonPatchDocument<Subscription>();
            
            sub.Replace(e => e.IsExpired, true);
            
            _response.Result = await _sdc.PatchSubscription(sub, id);
            
            return _response;
        }

        private decimal CalculateTotalAmount(IEnumerable<SubscriptionListModel> subscriptionList)
        {
            var totalAmount = 0.0M;

            foreach(var subs in subscriptionList)
            {
                totalAmount += subs.Amount;
            }

            return totalAmount;
        }

        [HttpPost]
        [Route("create-dummy-subscription")]
        public async Task<ActionResult<ApiResponse>> CreateDummySubscription()
        {
            SubscriptionCreateModel model = new SubscriptionCreateModel()
            {
                AccountId = 2,
                SubscriptionName = "Test Dummy Subscriptions",
                SubscribedOnEmail = "Test@yopmail.com",
                BillingDate = DateTime.Now,
                RenewalDate = DateTime.Now.AddMonths(1),
                Amount = 599,
                RenewalCycle = 1
            };

            try
            {
                _response.Result = await _sdc.CreateAsync(_mapper.Map<Subscription>(model));

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }
    }
}