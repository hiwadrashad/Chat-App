using Chat_App_JWT_API.Configuration;
using Chat_App_Library.Interfaces;
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
        public JwtMiddleware(RequestDelegate next, IOptions<JwtConfig> appSettings, IDatabaseSingleton userService)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            StringValues token;
            context.Request.Headers.TryGetValue("Authorization", out token);


            if (token.Count != 0)
            {
                var formattedttoken = token.ToString()?.Split(" ").Last();
                attachUserToContext(context, _userService, formattedttoken);
            }

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IDatabaseSingleton userService, string token)
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

                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetRepository().GetUserById(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
