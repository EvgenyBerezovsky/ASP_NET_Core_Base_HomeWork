using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IProfileService _profileService;
        private readonly IOptions<JwtConfigurationOptions> _jwtConfigurationOptions;
        public AuthService(IProfileService profileService, IOptions<JwtConfigurationOptions> jwtConfigurationOptions)
        {
            _jwtConfigurationOptions = jwtConfigurationOptions ?? throw new ArgumentNullException(nameof(jwtConfigurationOptions));
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        }
        /// <summary>
        /// Verifies the user's login and password and returns a token if authentication is successful.
        /// </summary>
        /// <param name="loginUserDTO">Object with user credentials (login and password)</param>
        /// <returns>'JWT token' in case of successful login or 'null' if the username or password is incorrect</returns>
        public async Task<string?> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _profileService.GetUserByLoginAsync(loginUserDTO.Login);
            if (user == null ||
                user.Password != loginUserDTO.Password)
                throw new ArgumentException("Invalid login or password.");

            Claim[] claims = GenerateClaimsByUser(user);
            var token = GenerateJwtToken(claims);
            return token;
        }
        private string? GenerateJwtToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurationOptions.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfigurationOptions.Value.Issuer,
                audience: _jwtConfigurationOptions.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private Claim[] GenerateClaimsByUser(GetUserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login)
            };
            return claims.ToArray();
        }

    }
}
