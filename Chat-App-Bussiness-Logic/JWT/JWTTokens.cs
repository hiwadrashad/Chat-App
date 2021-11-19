using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Bussiness_Logic.JWT
{
    public class JWTTokens
    {

        private readonly JwtConfig _jwtConfig;
        private IDatabaseSingleton _databaseSingleton;
        public JWTTokens (JwtConfig optionsMonitor, IDatabaseSingleton databaseSingleton)
        {
            _jwtConfig = optionsMonitor;
            _databaseSingleton = databaseSingleton;
        }
        public async Task<AuthResult> GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("Email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            //var jwtToken = jwtTokenHandler.WriteToken(token);
            var jwtToken = GenerateSimpleJwtToken(user);

            var refreshToken = new RefreshToken()
            {
                jwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString()
            };

            await _databaseSingleton.GetRepository().AddRefreshToken(refreshToken);

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }


        public string GenerateSimpleJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("Email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("CreationDate",DateTime.Now.ToString()),
                    new Claim("ExpirationDate",DateTime.Now.AddSeconds(30).ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
