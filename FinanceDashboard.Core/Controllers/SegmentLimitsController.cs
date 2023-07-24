using AutoMapper;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using System.Net;
using FinanceDashboard.Core.Security;
using FinanceDashboard.Data.DataControllers;
using FinanceDashboard.Models.Models.SegmentLimits;

namespace FinanceDashboard.Core.Controllers
{
    public class SegmentLimitsController : BaseController
    {
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly SeggmentLimitsDataContorller _sdc;
        public AccountDataController _adc;

        public SegmentLimitsController(AccountDataController adc, SeggmentLimitsDataContorller sdc)
        {
            _adc = adc;
            _mapper = new ObjectMapper().Mapper;
            _response = new();
            _sdc = sdc;
        }

        /// <summary>
        /// Get incomes for all users
        /// </summary>nn
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this to limit your output, by default it is set to 1000</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetSegmnetLimitDetails(int? limit = 1000, string? includeChildProperty = null)
        {
            try
            {
                _response.Result = _mapper.Map<List<SegmentLimitsListModel>>(await _sdc.GetAllAsync(includeChildProperties: includeChildProperty));

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

        /// <summary>
        /// Get income by account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="limit"></param>
        /// <param name="includeChildProperty"></param>
        /// <returns></returns>
        public async Task<CustomModelApiResponse<List<SegmentLimitsListModel>>> GetSegmnetLimitDetailsByAccountId(int? accountId, int? limit = 1000, string? includeChildProperty = null)
        {
            CustomModelApiResponse<List<SegmentLimitsListModel>> _response = new();

            if (accountId is null)
                accountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = _mapper.Map<List<SegmentLimitsListModel>>(await _sdc.GetAllAsync(x => x.AccountId == accountId, includeChildProperties: includeChildProperty));

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
        
        /// <summary>
        /// Create new income for logged in user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateLimits(SegmentLimitsCreateModel model)
        {
            SegmentLimits newLimits = _mapper.Map<SegmentLimits>(model);
            newLimits.AccountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = await _sdc.CreateAsync(newLimits);

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

        /// <summary>
        /// Create dummy income for logged in user
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> CreateDummyLimits()
        {
            SegmentLimitsCreateModel model = new SegmentLimitsCreateModel()
            {
                SubscriptionLimit = 15000.0M,
                EmiLimit = 15000.0M
            };

            SegmentLimits newLimits = _mapper.Map<SegmentLimits>(model);
            newLimits.AccountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = await _sdc.CreateAsync(newLimits);

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
    }
}