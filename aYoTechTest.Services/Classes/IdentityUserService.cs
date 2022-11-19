using aYoTechTest.BR.Classes;
using aYoTechTest.BR.Services.Interfaces;
using aYoTechTest.BR.ViewModels;
using aYoTechTest.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace aYoTechTest.Services.Classes
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<aYoTechTestUser> _userManager;
        private readonly ApiSetting _apiSetting;
        public IdentityUserService(
            UserManager<aYoTechTestUser> userManager,
            IOptions<ApiSetting> apiSetting
        )
        {
            _userManager = userManager;
            _apiSetting = apiSetting.Value;
        }


        public async Task<ApiAuthResponse> AuthenticateUser(ApiAuthRequest model)
        {
            var _user = await _userManager.FindByNameAsync(model.Username);

            if (_user != null)
            {
                bool _isValid = await _userManager.CheckPasswordAsync(_user, model.Password);

                if (_isValid)
                {
                    string _token = GenerateUserJwtToken(_user);
                    return new ApiAuthResponse()
                    {
                        Email = _user.Email,
                        FullName = _user.FullName,
                        Id = _user.Id,
                        Token = _token,
                        Username = _user.UserName
                    };
                }
            }

            return default;
        }

        public async Task<aYoTechTestUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        private string GenerateUserJwtToken(aYoTechTestUser user)
        {
            var tokenSecHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_apiSetting.TokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.WindowsAccountName, user.FullName),
                    new Claim("IssueDate", DateTime.Now.ToString())
                }),
                Expires = DateTime.Now.AddDays(1),
                IssuedAt = DateTime.Now,
                Issuer = _apiSetting.TokenIssuer,
                Audience = _apiSetting.TokenAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenSecHandler.CreateToken(tokenDescriptor);
            return tokenSecHandler.WriteToken(token);
        }
    }
}
