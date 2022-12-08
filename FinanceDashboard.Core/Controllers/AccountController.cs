using AutoMapper;
using FinanceDashboard.Models.Account;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using System.Net;
using System.Threading;
using FinanceDashboard.Utilities.EncryptorsDecryptors;
using FinanceDashboard.Models.PreLogon;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FinanceDashboard.Core.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace FinanceDashboard.Core.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AccountDataController _adc;
        private readonly IPasswordMethods _passwordMethods;
        protected readonly ApiResponse _response;
        private IMapper _mapper;

        public AccountController(AccountDataController adc, IPasswordMethods passwordMethods, IHttpContextAccessor httpContextAccessor)
        {
            _adc = adc;
            _response = new();
            _mapper = new ObjectMapper().Mapper;
            _passwordMethods = passwordMethods;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task<ApiResponse> GetUserByEmail(string email)
        {
            if (true)
            {
                _response.Result = Thread.CurrentPrincipal;
                return _response;
            }

            try
            {
                _response.Result = await GetAccountDetailByEmail(email);

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

        public async Task<ApiResponse> GetAllUsers()
        {
            var user = Thread.CurrentPrincipal;

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

        public async Task<ApiResponse> LogonAccount(AccountLogonModel model)
        {
            var account = await GetAccountDetailByEmail(model.Email);

            bool IsAccountValid = _passwordMethods.ComparePassword(model.Password, account.PasswordHash, account.HashingSalt);

            if (IsAccountValid)
            {
                var token = await CreateToken(account);

                _response.Result = token;

                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }

            return _response;
        }
        public async Task<ApiResponse> RegisterAccount(RegisterAccountModel model)
        {
            Account account = await MapModelToAccount(model);

            try
            {
                _response.Result = await _adc.CreateUserAsync(account);

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

        /// <summary>
        /// Maps the register account model to account entity and generated the hasing salt and password hash
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Mapped account entity</returns>
        private async Task<Account> MapModelToAccount(RegisterAccountModel model)
        {
            var passwordSalt = _passwordMethods.GenerateSalt();

            var passwordHash = _passwordMethods.GetHash(model.Password, passwordSalt);

            Account account = _mapper.Map<Account>(model);

            account.Id = Guid.NewGuid();
            account.HashingSalt = passwordSalt;
            account.PasswordHash = passwordHash;
            account.PasswordHashHistory = $"{passwordHash},";
            account.CreatedOn = DateTime.Now;

            return account;
        }

        private async Task<string> CreateToken(AccountDetailModel account)
        {            
            if (account.IsLocked ?? false)
                throw new Exception("Account Locked Exception.");

            if (account.SecondFactorValidated ?? false)
                throw new Exception("Requires second factor.");
            
            if (account.DeletedOn != null)
                throw new Exception("Account deleted.");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../FinanceDashboard.Service/appsettings.json").Build();
            var secretKey = configuration.GetSection("ApiSettings:SecretJWTKey").Value;

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var user = new FDPrincipal(account.Name, account.AccountId, account.Email, jwtToken);

            _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(user);

            Thread.CurrentPrincipal = user as FDPrincipal;
            
            return jwtToken;
        }

        private async Task SetCurrentPrincipal(FDPrincipal principal)
        {
            Thread.CurrentPrincipal = principal as FDPrincipal;
        }

        private async Task<AccountDetailModel> GetAccountDetailByEmail(string email)
        {
            return _mapper.Map<AccountDetailModel>(await _adc.GetAsync(x => x.Email == email));
        }
    }
}

//var principalType = Thread.CurrentPrincipal.GetType().Name;
//// principalType = WindowsPrincipal
//// Thread.CurrentThread.ManagedThreadId = 11

//await Task.Run(() =>
//{
//    // Tried putting await Task.Yield() here but didn't help

//    Thread.CurrentPrincipal = new UserPrincipal(Thread.CurrentPrincipal.Identity);
//    principalType = Thread.CurrentPrincipal.GetType().Name;
//    // principalType = UserPrincipal
//    // Thread.CurrentThread.ManagedThreadId = 10
//});
//principalType = Thread.CurrentPrincipal.GetType().Name;
//// principalType = WindowsPrincipal (WHY??)
//// Thread.CurrentThread.ManagedThreadId = 10
