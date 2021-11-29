using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.JWT;
using Chat_App_Library.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Bussiness_Logic.DependencyInjection
{
    public class ReturnCredentialsDependencyInjection
    {
        public static IDatabaseSingleton _databaseSingleton;
        public static IOptionsMonitor<JwtConfig> _optionsMonitor;
        public static TokenValidationParameters _tokenValidationParameters;
        public ReturnCredentialsDependencyInjection(IDatabaseSingleton databaseSingleton, IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParameters)
        {
            _databaseSingleton = databaseSingleton;
            _optionsMonitor = optionsMonitor;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public static CredentialDependencies GetDependancies()
        {
            return new CredentialDependencies()
            {
                _databaseSingleton = _databaseSingleton,
                _optionsMonitor = _optionsMonitor,
                _tokenValidationParameters = _tokenValidationParameters
            };
        }
    }

    public class CredentialDependencies
    {
        public IDatabaseSingleton _databaseSingleton { get; set; }
        public IOptionsMonitor<JwtConfig> _optionsMonitor { get; set; }
        public TokenValidationParameters _tokenValidationParameters { get; set; }
    }
}
