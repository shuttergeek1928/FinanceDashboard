using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Authorization;
using FinanceDashboard.Models.Account;
using FinanceDashboard.Models.Models.PreLogon;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceDashboard.Core.Controllers
{
    public class AuthenticateController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<PreLogonResponse> Login(PreLogonModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new PreLogonResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }

            return new PreLogonResponse { Status = "Failed", Message = "Failed to login."};
        }

        public async Task<PreLogonResponse> Register(AccountCreateModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);

            if (userExists != null)
                return new PreLogonResponse { Status = "Error", Message = "User already exists!" };

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.PasswordHash);

            if (!result.Succeeded)
                return new PreLogonResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            return new PreLogonResponse { Status = "Success", Message = "User created successfully!" };
        }

        public async Task<PreLogonResponse> RegisterAdmin(AccountCreateModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);

            if (userExists != null)
                return new PreLogonResponse { Status = "Error", Message = "User already exists!" };

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.PasswordHash);

            if (!result.Succeeded)
                return new PreLogonResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            if (!await _roleManager.RoleExistsAsync(Constants.Admin))
                await _roleManager.CreateAsync(new IdentityRole(Constants.Admin));

            if (!await _roleManager.RoleExistsAsync(Constants.User))
                await _roleManager.CreateAsync(new IdentityRole(Constants.User));

            if (await _roleManager.RoleExistsAsync(Constants.Admin))
            {
                await _roleManager.AddToRoleAsync(user, Constants.Admin);
            }

            return new PreLogonResponse { Status = "Success", Message = "User created successfully!" };
        }
    }
}