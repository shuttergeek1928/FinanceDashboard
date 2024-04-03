using AutoMapper;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using System.Net;
using FinanceDashboard.Core.Security;
using FinanceDashboard.Data.DataControllers;
using Microsoft.AspNetCore.JsonPatch;
using FinanceDashboard.Models.Models.Emi;

namespace FinanceDashboard.Core.Controllers
{
    public class EmiController : BaseController
    {
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly EmiDataContoller _edc;
        public AccountDataController _adc;

        public EmiController(AccountDataController adc, EmiDataContoller edc)
        {
            _adc = adc;
            _mapper = new ObjectMapper().Mapper;
            _response = new();
            _edc = edc;
        }

        /// <summary>
        /// Get emis for all users
        /// </summary>nn
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this to limit your output, by default it is set to 1000</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetEmiDetails(int? limit = 1000, string? includeChildProperty = null)
        {
            try
            {
                _response.Result = _mapper.Map<List<EmiListModel>>(await _edc.GetAllAsync(includeChildProperties: includeChildProperty));

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
        /// Get Emi by account id
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="limit"></param>
        /// <param name="includeChildProperty"></param>
        /// <returns></returns>
        public async Task<CustomModelApiResponse<List<EmiListModel>>> GetEmiByAccountId(int? accountId, int? limit = 1000, string? includeChildProperty = null)
        {
            CustomModelApiResponse<List<EmiListModel>> _response = new();

            if (accountId is null)
                accountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = _mapper.Map<List<EmiListModel>>(await _edc.GetAllAsync(x => x.AccountId == accountId, includeChildProperties: includeChildProperty));

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
        /// Create new Emi for logged in user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateEmi(EmiCreateModel model)
        {
            EMI newEmi = _mapper.Map<EMI>(model);
            newEmi.AccountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = await _edc.CreateAsync(newEmi);

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

        public async Task<ApiResponse> UpdateEmi(EmiUpdateModel model, Guid id)
        {
            JsonPatchDocument<EMI> patchModel = new JsonPatchDocument<EMI>();

            patchModel.Replace(a => a.CompletionDate, model.CompletionDate);
            patchModel.Replace(a => a.IsCompleted, model.IsCompleted);
            patchModel.Replace(a => a.CanceledOn, model.CancelledOn);
            patchModel.Replace(a => a.CanceledBy, model.CancelledBy);
            patchModel.Replace(a => a.LastUpdate, model.LastUpdated);
            patchModel.Replace(a => a.LastUpdateBy, model.LastUpdateBy);

            _response.Result = await _edc.PatchEmi(patchModel, id);
            return _response;
        }

        /// <summary>
        /// Create dummy emi for logged in user
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> CreateDummyEmi()
        {
            EmiCreateModel model = new EmiCreateModel()
            {
                EmiName = "LoanA",
                Amount = 10000M,
                InterestRate = 15,
                GstRate = 10,
                BillingDate = DateTime.UtcNow,
                InstallmentDate = DateTime.UtcNow.Day,
                CompletionDate = DateTime.UtcNow.AddMonths(6)
            };

            EMI newEmi = _mapper.Map<EMI>(model);
            newEmi.AccountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = await _edc.CreateAsync(newEmi);

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