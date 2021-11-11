using Chat_App_JWT_API.Configuration;
using Chat_App_JWT_API.JWT;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_JWT_API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _appSettings;
        private readonly IDatabaseSingleton _userService;
        private IRepository _repo;
        private readonly JwtConfig _jwtConfig;
        private JWTTokens _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly IOptionsMonitor<JwtConfig> _optionsMonitor;
        public JwtMiddleware(RequestDelegate next, IOptions<JwtConfig> appSettings, TokenValidationParameters tokenValidationParameters, IOptionsMonitor<JwtConfig> optionsMonitor, IDatabaseSingleton userService)
        {
            _optionsMonitor = optionsMonitor;
            _next = next;
            _repo = userService.GetRepository();
            _appSettings = appSettings.Value;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenGenerator = new JWTTokens(_jwtConfig, userService);
            _tokenValidationParams = tokenValidationParameters;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            StringValues token;
            if (AuthResultSingleton.GetSingleton().GetAuth() != null)
            {
                token = AuthResultSingleton.GetSingleton().GetAuth().Token;
            }
            else
            {
                context.Request.Headers.TryGetValue("Authorization", out token);
            }

            if (token.Count != 0)
            {
                var formattedttoken = token.ToString()?.Split(" ").Last();
                if (await generateAndAttachNewJWTToken(context, _userService, formattedttoken))
                {
                    attachUserToContext(context, _userService, formattedttoken);
                }

            }

            await _next(context);
        }

        private async Task<bool> generateAndAttachNewJWTToken(HttpContext context, IDatabaseSingleton userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                var user = userService.GetRepository().GetUserById(userId);
                JWTVerification jwtgenratorandverifier = new JWTVerification(userService, _optionsMonitor, _tokenValidationParams);
                TokenRequest request = new TokenRequest();
                request.Token = AuthResultSingleton.GetSingleton().GetAuth().Token;
                request.RefreshToken = AuthResultSingleton.GetSingleton().GetAuth().RefreshToken;
                var generatedjwt = await jwtgenratorandverifier.VerifyAndGenerateToken(request);
                if (generatedjwt.Success == false)
                {
                    return false;
                }
                else
                {
                    AuthResultSingleton.GetSingleton().SetAuth(generatedjwt);
                    context.Request.Headers["Authorization"] = generatedjwt.Token;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private bool attachUserToContext(HttpContext context, IDatabaseSingleton userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                context.Items["User"] = userService.GetRepository().GetUserById(userId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
