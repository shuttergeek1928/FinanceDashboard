using AutoMapper;
using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Models;
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
        private readonly IUserDataController _userDataController;
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        public SubscriptionApiController(IUserDataController userDataController, ISucbscriptionDataController sucbscriptionDataController, IMapper mapper)
        {
            _sucbscriptionDataController = sucbscriptionDataController;
            _userDataController = userDataController;
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
        [Route("get-all-subscription-by-accountId")]
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
                Amount = 500,
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