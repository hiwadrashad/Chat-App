using Castle.Core.Configuration;
using Chat_App__JWT_API.Controllers;
using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.DependencyInjection;
using Chat_App_Bussiness_Logic.Services;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Chat_App_Logic.Mocks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chat_App_Unit_Tests
{
    public class CredentialsTests : MockData
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IDatabaseSingleton _databaseSingleton;
        private readonly IRepository _repo;
        private readonly IChatService _chatService;
        private readonly ICredentialsService _credentialsService;
        private readonly ICredentialsController _credentialsController;
        public CredentialsTests()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(@"E:\Programming\Chat-App\Chat-App-JWT-API")

            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();
            var services = new ServiceCollection();
            services.Configure<JwtConfig>(_configuration.GetSection("JwtConfig"));
            DatabaseSingleton.GetSingleton().SetRepository(new MockingRepository());
            services.AddSingleton(typeof(IDatabaseSingleton), DatabaseSingleton.GetSingleton());
            services.AddSingleton(typeof(IChatService), new ChatService(DatabaseSingleton.GetSingleton()));
            services.AddSingleton(typeof(IGroupService), new GroupService(DatabaseSingleton.GetSingleton()));
            services.AddSingleton(typeof(IInvitationService), new InvitationService(DatabaseSingleton.GetSingleton()));
            services.AddSingleton(typeof(IRepository), DatabaseSingleton.GetSingleton().GetRepository());
            services.AddSingleton<ICredentialsService, CredentialsService>();
            services.AddSingleton<ICredentialsController, CredentialsController>();

            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);

            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParameters;
            });

            var serviceProvider = services.BuildServiceProvider();

            _databaseSingleton = serviceProvider.GetService<IDatabaseSingleton>();
            _chatService = serviceProvider.GetService<IChatService>();
            _credentialsService = serviceProvider.GetService<ICredentialsService>();
            _credentialsController = serviceProvider.GetService<ICredentialsController>();

            _repo = _databaseSingleton.GetRepository();

        }

        [Fact]
        public async Task LOGIN()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.Login("password", MOCKRETURN_USER) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task MAKE_USER_ADMIN()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.UpdateUserData(MOCKRETURN_USER));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.MakeUserAdmin(MOCKRETURN_USERTOASCEND) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task REGISTER()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var NewUser = new User() {

                Email = "REGISTER",
                Id = 100,
                Name = "REGISTER",
                Salt = "REGISTER",
                Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "REGISTER"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password", "SALT")),
                Username = "REGISTER"
            };
            var Return = await _credentialsController.Register("password",NewUser) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task GET_USERS_BY_EMAIL()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.GetUsersByEmail("user@example.com", 1) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]

        public async Task GET_USERS()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.GetUsers(1) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]

        public async Task GET_USER_BY_ID()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.GetUserById(1,1) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]

        public async Task GET_USERS_BY_NAME()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.GetUsersByName("test", 1) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]

        public async Task UPDATE_USER_DATA()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.UpdateUserData(MOCKRETURN_USER));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.UpdateUserData(MOCKRETURN_USER) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]

        public async Task BanUser()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = _credentialsService;
            var Return = await _credentialsController.BanUser(0,1) as OkObjectResult;
            Assert.NotNull(Return);
        }
    }
}
