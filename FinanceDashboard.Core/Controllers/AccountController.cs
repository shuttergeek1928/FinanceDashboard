using AutoMapper;
using FinanceDashboard.Models.Account;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using System.Net;
using FinanceDashboard.Utilities.EncryptorsDecryptors;
using FinanceDashboard.Models.PreLogon;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceDashboard.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FinanceDashboard.Core.Controllers
{
    public class AccountController
    {
        private readonly AccountDataController _adc;
        private readonly IPasswordMethods _passwordMethods;
        protected readonly ApiResponse _response;
        private IMapper _mapper;

        public AccountController(AccountDataController adc, IPasswordMethods passwordMethods)
        {
            _adc = adc;
            _response = new();
            _mapper = new ObjectMapper().Mapper;
            _passwordMethods = passwordMethods;
        }

        public async Task<ApiResponse> GetUserByEmail(string email)
        {
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

        [Authorize]
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

        public async Task<ApiResponse> LogonAccount(AccountLogonModel model)
        {
            var account = await GetAccountDetailByEmail(model.Email);

            bool IsAccountValid = _passwordMethods.ComparePassword(model.Password, account.PasswordHash, account.HashingSalt);

            if (IsAccountValid)
            {
                var token = CreateToken(account);

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

        private string CreateToken(AccountDetailModel account)
        {
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

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private async Task<AccountDetailModel> GetAccountDetailByEmail(string email)
        {
            return _mapper.Map<AccountDetailModel>(await _adc.GetAsync(x => x.Email == email));
        }
    }
}
