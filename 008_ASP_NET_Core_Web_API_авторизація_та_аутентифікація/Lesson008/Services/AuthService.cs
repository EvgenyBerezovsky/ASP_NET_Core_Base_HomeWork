using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lesson008.Models;

namespace Lesson008.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userSrvice;
        private readonly IOptions<JwtConfigurationOptions> _configurationOptions;

        public AuthService(IOptions<JwtConfigurationOptions> configurationOptions, IUserService userService)
        {
            _configurationOptions= configurationOptions ?? throw new ArgumentNullException(nameof(configurationOptions));
            _userSrvice = userService;
        }
        public string? LoginUser(LoginRequest loginRequest)
        {
            var _users = _userSrvice.GetAllUsers();
            var user = _users.FirstOrDefault(u => u.Login == loginRequest.Login && u.Password == loginRequest.Password);
            
            if (user is null)
            {
                return null;
            }

            Claim[] claims = GenerateClaimsByUser(user);

            var token = GenerateJwtToken(claims);
            return token;
        }

        private string GenerateJwtToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationOptions.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configurationOptions.Value.Issuer,
                audience: _configurationOptions.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] GenerateClaimsByUser(User user)
        {
            return new Claim[]
            {
                new Claim("Login", user.Login),
                new Claim(ClaimTypes.Role, user.Position.ToString()),
                new Claim("Department", user.Department.ToString()),
            };
        }
    }
}
