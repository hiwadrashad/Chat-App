using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.Services;
using Chat_App_Library.Interfaces;
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
    public class InvitationTests : MockData
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IDatabaseSingleton _databaseSingleton;
        private readonly IRepository _repo;
        private readonly IGroupService _groupService;
        private readonly ICredentialsService _credentialsService;
        public InvitationTests()
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
            _groupService = serviceProvider.GetService<IGroupService>();
            _credentialsService = serviceProvider.GetService<ICredentialsService>();

            _repo = _databaseSingleton.GetRepository();

        }


        [Fact]
        public async Task SEND_INVITATION()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var invitationservice = new InvitationService(_databaseSingleton);
            var controller = new Chat_App_JWT_API.Controllers.InvitationController(invitationservice);
            var Return = await controller.SendInvitation(1, Chat_App_Library.Enums.GroupType.groupchat, 1, MOCKRETURN_INVITATION) as OkObjectResult;
            Assert.NotNull(Return);
        }

        /// <summary>
        /// Test only works in debugging mode!
        /// </summary>
        [Fact]
        public async Task ACCEPT_INVITATION()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            MockingRepository.Setup(a => a.GetGroupChats()).Returns(MOCKRETURN_GROUPCHATS);
            MockingRepository.Setup(a => a.GetSingleUserChat()).Returns(MOCKRETURN_SINGLE_USER_CHATS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var invitationservice = new InvitationService(_databaseSingleton);
            var controller = new Chat_App_JWT_API.Controllers.InvitationController(invitationservice);
            var Return = await controller.AcceptInvitation(0, MOCKRETURN_INVITATION) as OkObjectResult;
            Assert.NotNull(Return);
        }
        /// <summary>
        /// Test only works in debugging mode!
        /// </summary>
        [Fact]
        public async Task DECLINE_INVITATION()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUsers()).Returns(MOCKRETURN_USERS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var invitationservice = new InvitationService(_databaseSingleton);
            var controller = new Chat_App_JWT_API.Controllers.InvitationController(invitationservice);
            var Return = await controller.DeclineInvitation(0, MOCKRETURN_INVITATION) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]

        public async Task GET_GROUP()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetGeneralChat()).Returns(MOCKRETURN_GENERALCHATS);
            MockingRepository.Setup(a => a.GetGroupChats()).Returns(MOCKRETURN_GROUPCHATS);
            MockingRepository.Setup(a => a.GetSingleUserChat()).Returns(MOCKRETURN_SINGLE_USER_CHATS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var invitationservice = new InvitationService(_databaseSingleton);
            var controller = new Chat_App_JWT_API.Controllers.InvitationController(invitationservice);
            var Return = await controller.DeclineInvitation(0, MOCKRETURN_INVITATION) as OkObjectResult;
            Assert.NotNull(Return);
        }
    }
}
