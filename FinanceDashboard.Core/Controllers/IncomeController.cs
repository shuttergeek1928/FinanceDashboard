using AutoMapper;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using System.Net;
using FinanceDashboard.Core.Security;
using FinanceDashboard.Data.DataControllers;
using FinanceDashboard.Models.Models.Income;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace FinanceDashboard.Core.Controllers
{
    public class IncomeController : BaseController
    {
        private readonly IMapper _mapper;
        protected readonly ApiResponse _response;
        private readonly IncomeDataContoller _idc;
        public AccountDataController _adc;

        public IncomeController(AccountDataController adc, IncomeDataContoller idc)
        {
            _adc = adc;
            _mapper = new ObjectMapper().Mapper;
            _response = new();
            _idc = idc;
        }

        /// <summary>
        /// Get incomes for all users
        /// </summary>nn
        /// <param name="includeChildProperty"></param>
        /// <param name="limit">Use this to limit your output, by default it is set to 1000</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetIncomeDetails(int? limit = 1000, string? includeChildProperty = null)
        {
            try
            {
                _response.Result = _mapper.Map<List<IncomeListModel>>(await _idc.GetAllAsync(includeChildProperties: includeChildProperty));

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
        public async Task<CustomModelApiResponse<List<IncomeListModel>>> GetIncomesByAccountId(int? accountId, int? limit = 1000, string? includeChildProperty = null)
        {
            CustomModelApiResponse<List<IncomeListModel>> _response = new();

            if (accountId is null)
                accountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = _mapper.Map<List<IncomeListModel>>(await _idc.GetAllAsync(x => x.AccountId == accountId, includeChildProperties: includeChildProperty));

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
        public async Task<ApiResponse> CreateIncome(IncomeCreateModel model)
        {
            Income newIncome = _mapper.Map<Income>(model);
            newIncome.AccountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = await _idc.CreateAsync(newIncome);

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

        public async Task<decimal> GetBalance()
        {
            var allIncomes = await GetIncomesByAccountId(User.FDIdentity.AccountId);

            if (allIncomes.Result == null)
                throw new Exception("No income found");

            IncomeDetailModel income = _mapper.Map<IncomeDetailModel>(allIncomes.Result.OrderByDescending(x => x.CredtiDate).FirstOrDefault());
            return income.IncomeBalance;
        }

        /// <summary>
        /// Create dummy income for logged in user
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> CreateDummyIncome()
        {
            IncomeCreateModel model = new IncomeCreateModel()
            {
                IncomeName = "Test income (Hexaware)",
                Amount = 30000.00M,
                CreditCycle = "Monthly",
                Creditor = "Hexaware Test",
                CredtiDate = new DateTime(2023, 01, 30),
                IncomeBalance = 30000.00M
            };

            Income newIncome = _mapper.Map<Income>(model);
            newIncome.AccountId = User.FDIdentity.AccountId;

            try
            {
                _response.Result = await _idc.CreateAsync(newIncome);

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