using AutoMapper;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;
using FinanceDashboard.Service.Models.Account;
using FinanceDashboard.Service.Models.Subscription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace FinanceDashboard.Service.ApiControllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class SubscriptionApiController : ControllerBase
    {
        private readonly ISucbscriptionDataController _sucbscriptionDataController;
        private readonly IAccountDataController _accountDataController;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        public SubscriptionApiController(IAccountDataController accountDataController, ISucbscriptionDataController sucbscriptionDataController, IMapper mapper)
        {
            _sucbscriptionDataController = sucbscriptionDataController;
            _accountDataController = accountDataController;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllSubscription(string? includeChildProperty = null)
        {
            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sucbscriptionDataController.GetAllAsync(includeChildProperties: includeChildProperty));

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
        [Route("get/all-subscription-by-accountId")]
        public async Task<ActionResult<ApiResponse>> GetAllSubscriptionByAccountId(int? accountId, string? includeChildProperty = null)
        {
            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sucbscriptionDataController.GetAllAsync(x => x.User.AccountId == accountId, includeChildProperties: includeChildProperty));

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
        [Route("update/subscription/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> UpdateSubscription(SubscriptionUpdateModel model, Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Subscription subscription = _mapper.Map<Subscription>(await _sucbscriptionDataController.GetAsync(x => x.Id == id));

            if (subscription == null)
                throw new Exception($"Subscription of id: {id} does not exist");

            try
            {
                subscription.LastUpdate = DateTime.Now;

                subscription.LastUpdateBy = subscription.AccountId;

                _response.Result = await _sucbscriptionDataController.UpdateSubscription(_mapper.Map<SubscriptionUpdateModel, Subscription>(model, subscription));

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
        [Route("create/new-subscription")]
        public async Task<ActionResult<ApiResponse>> CreateNewSubscription(SubscriptionCreateModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _accountDataController.GetAsync(x => x.AccountId == model.AccountId);

            if (account == null)
                throw new Exception($"Account id: {model.AccountId} does not exist");

            try
            {
                _response.Result = await _sucbscriptionDataController.CreateAsync(_mapper.Map<Subscription>(model));

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
        [Route("delete/subscription/{id:Guid}")]
        public async Task<ActionResult<ApiResponse>> DeleteSubscriptionById(Guid id)
        {
            Subscription subscription = _mapper.Map<Subscription>(await _sucbscriptionDataController.GetAsync(x => x.Id == id));

            if (subscription == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Result = $"Subscription of id: {id} not found";
                return _response;
            }

            try
            {
                await _sucbscriptionDataController.RemoveAsync(subscription);
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
        [Route("get/totalSubscriptionValue/byAccountId/{accountId:int}")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int? accountId)
        {
            var account = await _accountDataController.GetAsync(x => x.AccountId == accountId);

            if (account == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Result = $"Account id: {accountId} does not exist";
                return _response;
            }

            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sucbscriptionDataController.GetAllAsync(x => x.User.AccountId == accountId));

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
        [Route("get/allActiveOrExpiredSubscriptions/byAccountId")]
        public async Task<ActionResult<ApiResponse>> GetTotalAmount(int accountId, bool? isExpired = false)
        {
            var account = await _accountDataController.GetAsync(x => x.AccountId == accountId);

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
                        await _sucbscriptionDataController.GetAllAsync(x => 
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
                RenewalDate = DateTime.Now.AddDays(30),
                Amount = 599,
                RenewalCycle = 1
            };

            try
            {
                _response.Result = await _sucbscriptionDataController.CreateAsync(_mapper.Map<Subscription>(model));

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