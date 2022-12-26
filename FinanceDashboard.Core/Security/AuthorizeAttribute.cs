using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Mvc;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;

namespace FinanceDashboard.Core.Security
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();
        private AccountDataController _adc;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _adc = context.HttpContext.RequestServices.GetService<AccountDataController>();
            string bearerToken = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value;
            if (bearerToken == null)
                throw new Exception("UnAuthorized");

            bearerToken = bearerToken.Substring(7);
            var payload = GetPayLoadData(bearerToken);
            var email = payload.FirstOrDefault(x => x.Key == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value.ToString();
            var account = GetAccountDetailsByEmail(email);

            var user = new FDIdentity("FDAuth", true, account.Name, account.AccountId, email, bearerToken);
            context.HttpContext.User = new ClaimsPrincipal(user);
            Thread.CurrentPrincipal = new FDPrincipal(account.Name, account.AccountId, account.Email, bearerToken);
        }

        private JwtPayload GetPayLoadData(string token)
        {
            var tokenData = _jwtHandler.ReadJwtToken(token);
            return tokenData.Payload;
        }

        private Account GetAccountDetailsByEmail(string email)
        {
            //var _accountDataController = DependencyResolver.Current.GetService<AccountDataController>();
            return _adc.GetAccountByEmail(email);
        }
    }
}
