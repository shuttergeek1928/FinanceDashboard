using AutoMapper;
using FinanceDashboard.Models.Account;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using System.Net;

namespace FinanceDashboard.Core.Controllers
{
    public class AccountController
    {
        private readonly AccountDataController _adc;
        protected readonly ApiResponse _response;
        private IMapper _mapper;
        public AccountController(AccountDataController adc)
        {
            _adc = adc;
            _response = new();
            _mapper = new ObjectMapper().Mapper;
        }

        public async Task<ApiResponse> GetAllUsers()
        {
            try
            {
                IEnumerable<AccountListModel> accounts = _mapper.Map<List<AccountListModel>>(await _adc.GetAllAsync());

                _response.Result = accounts;

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

        public async Task<ApiResponse> CreateDummyUser()
        {

            AccountDetailModel user = new AccountDetailModel
            {
                Id = Guid.NewGuid(),
                Name = "Dummy Dummy",
                FirstName = "Dummy",
                LastName = "Dummy",
                Email = DateTime.Now.ToString(),
                PasswordHash = "Dummy",
                HashingSalt = "Dummy",
                PasswordHashHistory = "Dummy",
                MobileNumber = "9999999999",
                CreatedOn = DateTime.Now
            };

            try
            {
                _response.Result = await _adc.CreateUserAsync(_mapper.Map<Account>(user));

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
