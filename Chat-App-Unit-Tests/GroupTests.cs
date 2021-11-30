using Castle.Core.Configuration;
using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.Services;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Chat_App_Logic.Mocks;
using Chat_App_Unit_Tests.MockingDatabase;
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
    public class GroupTests : MockData
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IDatabaseSingleton _databaseSingleton;
        private readonly IRepository _repo;
        private readonly IGroupService _groupService;
        private readonly ICredentialsService _credentialsService;
        private IRepository _EF6Repo;
        public GroupTests()
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
            services.AddScoped<IRepository, TestRepository>();

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
            _EF6Repo = serviceProvider.GetService<IRepository>();

            _repo = _databaseSingleton.GetRepository();

        }


        [Fact]
        public async Task ADD_GROUP_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.AddGroupChat(MOCKRETURN_GROUPCHAT));
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.AddGroupChat("password",MOCKRETURN_GROUPCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task ADD_GROUP_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.AddGroupChat("password", MOCKRETURN_GROUPCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task ADD_SINGLE_USER_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.AddSingleUserChat(MOCKRETURN_SINGLE_USER_CHAT));
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var test = await controller.AddSingleUserChat("password", MOCKRETURN_SINGLE_USER_CHAT) as BadRequestObjectResult;
            var Return = await controller.AddSingleUserChat("password", MOCKRETURN_SINGLE_USER_CHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task ADD_SINGLE_USER_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var test = await controller.AddSingleUserChat("password", MOCKRETURN_SINGLE_USER_CHAT) as BadRequestObjectResult;
            var Return = await controller.AddSingleUserChat("password", MOCKRETURN_SINGLE_USER_CHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task ADD_GENERAL_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.AddGeneralChat(MOCKRETURN_GENERALCHAT));
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.AddGeneralChat(MOCKRETURN_GENERALCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task ADD_GENERAL_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.AddGeneralChat(MOCKRETURN_GENERALCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task DELETE_GENERAL_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.DeleteGeneralChat(MOCKRETURN_GENERALCHAT));
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.DeleteGeneralChat(1,MOCKRETURN_GENERALCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task DELETE_GENERAL_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.DeleteGeneralChat(1, MOCKRETURN_GENERALCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task DELETE_GROUP()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.DeleteGroup(MOCKRETURN_GROUPCHAT));
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.DeleteGroup(1, MOCKRETURN_GROUPCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task DELETE_GROUP_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.DeleteGroup(1, MOCKRETURN_GROUPCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task DELETE_SINGLE_PERSON_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.DeleteSiglePersonChat(MOCKRETURN_SINGLE_USER_CHAT));
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.DeleteSinglePersonChat(1, MOCKRETURN_SINGLE_USER_CHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task DELETE_SINGLE_PERSON_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.DeleteSinglePersonChat(1, MOCKRETURN_SINGLE_USER_CHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }


        [Fact]
        public async Task GET_GROUP_CHATS_BY_USER_ID()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.GetGroupChatsByUserId(a => a.Id == 1)).Returns(MOCKRETURN_GROUPCHATS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetGroupChatsByUserId(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_SINGLE_USER_CHATS_BY_USER_ID()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.GetSingleUserChatByUserId(a => a.Id == 1)).Returns(MOCKRETURN_SINGLE_USER_CHATS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetSingleUserChatByUserId(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_SINGLE_USER_CHATS_BY_USER_ID_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetSingleUserChatByUserId(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_GROUPS_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.GetGroupChats()).Returns(MOCKRETURN_GROUPCHATS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetGroupsChat(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_GROUPS_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetGroupsChat(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_GENERAL_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.GetGeneralChat()).Returns(MOCKRETURN_GENERALCHATS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetGeneralChat(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_GENERAL_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetGeneralChat(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_SINGLE_USER_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.GetSingleUserChatByUserId(a => a.Id == 1)).Returns(MOCKRETURN_SINGLE_USER_CHATS);
            _databaseSingleton.SetRepository(new MockingRepository());
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetSingleUserChatByUserId(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task GET_SINGLE_USER_CHAT_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            _EF6Repo.ClearAllDataSets();
            _EF6Repo.SeedMoqData();
            _databaseSingleton.SetRepository(_EF6Repo);
            var groupservice = new GroupService(_databaseSingleton);
            var controller = new Chat_App__JWT_API.Controllers.GroupController(_databaseSingleton, groupservice);
            var Return = await controller.GetSingleUserChatByUserId(1) as OkObjectResult;
            Assert.NotNull(Return);
        }
    }
}
