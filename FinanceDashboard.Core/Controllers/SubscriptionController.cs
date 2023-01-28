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
    public class SubscriptionController : BaseController
    {
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly SubscriptionDataController _sdc;
        public AccountDataController _adc;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IncomeController _ic;

        public SubscriptionController(AccountDataController adc, SubscriptionDataController sdc, IHttpContextAccessor httpContextAccessor, IncomeController ic)
        {
            _adc = adc;
            _mapper = new ObjectMapper().Mapper;
            _response = new();
            _sdc = sdc;
            _ic = ic;
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

            if (accountId is null)
                accountId = User.FDIdentity.AccountId;

            try
            {
                IEnumerable<SubscriptionListModel> subscriptions = _mapper.Map<List<SubscriptionListModel>>(await _sdc.GetAllAsync(
                    x => x.AccountId == accountId,
                    includeChildProperties: includeChildProperty));

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
            Subscription subscription = _mapper.Map<Subscription>(await _sdc.GetAsync(x => x.Id == id && x.AccountId == User.FDIdentity.AccountId));

            if (subscription == null)
                throw new Exception($"Subscription of id: {id} does not exist for the current logged in user - UnAuthorized area!");

            try
            {
                model.LastUpdate = DateTime.Now;

                model.LastUpdateBy = subscription.AccountId;

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

        public async Task<ApiResponse> CreateNewSubscription(SubscriptionCreateModel model, bool checkFirst = true)
        {
            Subscription newSubscription = _mapper.Map<Subscription>(model);
            newSubscription.AccountId = User.FDIdentity.AccountId;

            if (checkFirst && !await CheckLatestSubscriptionAmount(newSubscription.Amount))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotAcceptable;
                _response.Result = null;
                _response.Errors = new List<string> { "Can't add this as the new total subscription will be more than your limit" };
                return _response;
            }

            try
            {
                _response.Result = await _sdc.CreateAsync(newSubscription);

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

        public async Task<ApiResponse> GetTotalAmount()
        {
            return await CalculateTotalAmount(User.FDIdentity.AccountId);
        }

        public async Task<ApiResponse> GetTotalAmount(int? accountId)
        {
            return await CalculateTotalAmount(accountId);
        }

        public async Task<ApiResponse> GetActiveOrExpiredSubscription(bool? isExpired = false)
        {
            return await GetActiveOrExpiredSubscriptions(User.FDIdentity.AccountId, isExpired);
        }

        public async Task<ApiResponse> GetActiveOrExpiredSubscription(int accountId, bool? isExpired = false)
        {
            return await GetActiveOrExpiredSubscriptions(accountId, isExpired);
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
                SubscriptionName = "Test Dummy Subscriptions",
                SubscribedOnEmail = "Test@yopmail.com",
                BillingDate = DateTime.Now,
                RenewalDate = DateTime.Now.AddMonths(1),
                Amount = 599,
                RenewalCycle = 1
            };

            Subscription newSubscription = _mapper.Map<Subscription>(model);
            newSubscription.AccountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = await _sdc.CreateAsync(newSubscription);

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

        private async Task<ApiResponse> CalculateTotalAmount(int? accountId)
        {
            var totalAmount = 0.0M;

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

                foreach (var subs in subscriptions)
                {
                    totalAmount += subs.Amount;
                }

                _response.Result = totalAmount;

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

        private async Task<ApiResponse> GetActiveOrExpiredSubscriptions(int accountId, bool? isExpired = false)
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

        private async Task<bool> CheckLatestSubscriptionAmount(decimal amt)
        {
            var totalAmount = await CalculateTotalAmount(User.FDIdentity.AccountId);
            var incomeBalance = await _ic.GetIncomesByAccountId(User.FDIdentity.AccountId);

            return (decimal.Parse(totalAmount.Result.ToString()) + amt) <= 3000.0M;
        }
    }
}