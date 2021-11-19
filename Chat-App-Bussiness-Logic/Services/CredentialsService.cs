using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.JWT;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat_App_Bussiness_Logic.Services
{
    public class CredentialsService
    {
        private IDatabaseSingleton _databaseSingleton;
        private IRepository _repo;
        private readonly JwtConfig _jwtConfig;
        private JWTTokens _tokenGenerator;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly JWTVerification _jwtVerification;
        public CredentialsService(IDatabaseSingleton databaseSingleton, IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParameters)
        {
            _databaseSingleton = databaseSingleton;
            _repo = databaseSingleton.GetRepository();
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenGenerator = new JWTTokens(_jwtConfig, databaseSingleton);
            _tokenValidationParams = tokenValidationParameters;
            _jwtVerification = new JWTVerification(databaseSingleton, optionsMonitor, tokenValidationParameters);
        }

        public async Task<object> Login(User input, AuthResult jwtToken)
        {
            try
            {
                if (!(input.Role == Chat_App_Library.Enums.Role.Admin) && input.Banned == true)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "You are banned!"
                        },
                        Success = false
                    };
                }
                if (input == null)
                {
                    return new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                        "User not found!"
                        },
                        Success = false
                    };
                }
                else
                {
                    await Task.Run(() => AuthResultSingleton.GetSingleton().SetAuth(jwtToken));
                    await Task.Run(() =>UserSingleton.GetSingleton().SetUser(input));
                    return jwtToken;
                }
            }
            catch(Exception ex)
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        $"Something went wrong {ex.Message}"
                        },
                    Success = false
                };
            }
        }
    }
}
