using AutoMapper;
using FinanceDashboard.Models.Subscription;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using Microsoft.AspNetCore.Http;
using FinanceDashboard.Core.Security;

namespace FinanceDashboard.Core.Controllers
{
    public class SubscriptionController
    {
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly SubscriptionDataController _sdc;
        public AccountDataController _adc;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public SubscriptionController(AccountDataController adc, SubscriptionDataController sdc, IHttpContextAccessor httpContextAccessor)
        {
            _adc = adc;
            _mapper = new ObjectMapper().Mapper;
            _response = new();
            _sdc = sdc;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get all subscriptions for all users
        /// </summary>nn
        /// <param name="includeChildProperty"></param>
        /// <returns></returns>
        /// 
        public async Task<ApiResponse> GetAllSubscription(string? includeChildProperty = null)
        {
            //var jwtToken = _httpContextAccessor.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization");
            //var req = _httpContextAccessor.HttpContext.User;
            //var role = req.FindFirst(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            //var exp = long.Parse(req.FindFirst(x => x.Type == "exp").Value);

            var user = Thread.CurrentPrincipal;

            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(includeChildProperties: includeChildProperty));

                _response.Result = subscriptions;

                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> GetAllSubscriptionByAccountId(int? accountId, string? includeChildProperty = null)
        {
            var user = Thread.CurrentPrincipal;

            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(x => x.User.AccountId == accountId, includeChildProperties: includeChildProperty));

                _response.Result = subscriptions;

                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> GetSubscriptionById(Guid id, string? includeChildProperty = null)
        {
            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(x => x.Id == id, includeChildProperties: includeChildProperty));

                _response.Result = subscriptions;

                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> UpdateSubscription(SubscriptionUpdateModel model, Guid id)
        {
            Subscription subscription = _mapper.Map<Subscription>(await _sdc.GetAsync(x => x.Id == id));

            if (subscription == null)
                throw new Exception($"Subscription of id: {id} does not exist");

            try
            {
                subscription.LastUpdate = DateTime.Now;

                subscription.LastUpdateBy = subscription.AccountId;

                _response.Result = await _sdc.UpdateSubscription(_mapper.Map<SubscriptionUpdateModel, Subscription>(model, subscription));

                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> CreateNewSubscription(SubscriptionCreateModel model)
        {
            var account = await _adc.GetAsync(x => x.AccountId == model.AccountId);

            if (account == null)
                throw new Exception($"Account id: {model.AccountId} does not exist");

            try
            {
                _response.Result = await _sdc.CreateAsync(_mapper.Map<Subscription>(model));

                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> DeleteSubscriptionById(Guid id)
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
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> GetTotalAmount(int? accountId)
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

                return _response;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> GetActiveOrExpiredSubscription(int accountId, bool? isExpired = false)
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

                return _response;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;

                _response.Errors = new List<string>() { e.ToString() };
            }

            return _response;
        }

        public async Task<ApiResponse> ExpireSubscriptionsById(Guid id)
        {
            JsonPatchDocument<Subscription> sub = new JsonPatchDocument<Subscription>();

            sub.Replace(e => e.IsExpired, true);

            _response.Result = await _sdc.PatchSubscription(sub, id);

            return _response;
        }

        public async Task<ApiResponse> CreateDummySubscription()
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

                return _response;
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

            foreach (var subs in subscriptionList)
            {
                totalAmount += subs.Amount;
            }

            return totalAmount;
        }
    }
}