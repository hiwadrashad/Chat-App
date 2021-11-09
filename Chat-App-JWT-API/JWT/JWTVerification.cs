using Chat_App_JWT_API.Configuration;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App_JWT_API.JWT
{
    public class JWTVerification
    {
        private IDatabaseSingleton _databaseSingleton;
        private IRepository _repo;
        private readonly JwtConfig _jwtConfig;
        private JWTTokens _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParams;
        public JWTVerification(IDatabaseSingleton databaseSingleton, IOptionsMonitor<JwtConfig> optionsMonitor,
        TokenValidationParameters tokenValidationParameters)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenGenerator = new JWTTokens(_jwtConfig, databaseSingleton);
            _tokenValidationParams = tokenValidationParameters;
        }
        public async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenInVerification = JwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);
                
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                    {
                        return null;
                    }
                }

                var utcExpiryDate = long.Parse(tokenInVerification.Claims
                    .FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = Chat_App_Bussiness_Logic.Conversions.
                    DateTimeConversion.UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow) {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                             "Token has not yet expired"
                        }
                    };
                }

                var storedToken = _databaseSingleton.GetRepository()
                    .GetAllRefreshTokens().FirstOrDefault(a => a.Token == tokenRequest.Token);

                if (storedToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                             "Token does not exist"
                        }
                    };
                }

                if (storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                             "Token has been used"
                        }
                    };
                }

                if (storedToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                             "Token has been revoked"
                        }
                    };
                }

                var jti = tokenInVerification.Claims.FirstOrDefault(a => a.Type
                 == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.jwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                             "Token doesn't match"
                        }
                    };
                }

                storedToken.IsUsed = true;
                _databaseSingleton.GetRepository().UpdateRefreshToken(storedToken);

                var dbUser =  _databaseSingleton.GetRepository().GetUserById(storedToken.UserId);
                return await _tokenGenerator.GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
