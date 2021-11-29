using Chat_App__JWT_API.Controllers;
using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.Services;
using Chat_App_JWT_API.Controllers;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using Chat_App_Logic.Mocks;
using Chat_App_Logic.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chat_App_Unit_Tests
{
    public class ChatTests : MockData
    {
        private IConfiguration _configuration;
        private readonly IDatabaseSingleton _databaseSingleton;
        private readonly IRepository _repo;
        private readonly IChatService _chatService;
        private readonly ICredentialsService _credentialsService;
        public ChatTests()
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
            _chatService = serviceProvider.GetService<IChatService>();
            _credentialsService = serviceProvider.GetService<ICredentialsService>();

            _repo = _databaseSingleton.GetRepository();

        }
        [Fact]
        public async Task GET_MESSAGES()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<ChatDbContextRepository>();
            MockingRepository.Setup(a => a.GetMessages()).Returns(MOCKRETURN_MESSAGES);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.GetMessages(1) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task GET_MESSAGES_EF6()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<ChatDbContextRepository>();
            MockingRepository.Setup(a => a.GetMessages()).Returns(MOCKRETURN_MESSAGES);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.GetMessages(1) as OkObjectResult;
            Assert.NotNull(Return);
        }


        [Fact]
        public async Task GET_MESSAGES_BY_USER_ID()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetMessagesByUserId(a => a.Id == 1)).Returns(MOCKRETURN_MESSAGES);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.GetMessagesByuserId(1) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task DELETE_MESSAGE_GROUP()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.DeleteMessageGroup(MOCKRETURN_GROUPCHAT, 1));
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.DeleteMessageGroup(1, MOCKRETURN_GROUPCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task DELETE_MESSAGE_GENERAL()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.DeleteMessageGeneral(MOCKRETURN_GENERALCHAT, 1));
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.DeleteMessageGeneral(1, MOCKRETURN_GENERALCHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task DELETE_MESSAGE_SINGLE_USER()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.DeleteSiglePersonChat(MOCKRETURN_SINGLE_USER_CHAT));
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.DeleteMessageSingleUser(1, MOCKRETURN_SINGLE_USER_CHAT) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task UPDATE_MESSAGE_TO_GROUP_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetGroupChats()).Returns(MOCKRETURN_GROUPCHATS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.UpdateMessageToGroupChat(MOCKRETURN_MESSAGE,1));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.UpdateMessageToGroupChat(0, MOCKRETURN_MESSAGE) as OkObjectResult;
            Assert.NotNull(Return);
        }

        /// <summary>
        /// Test only works in debugging mode!
        /// </summary>
        /// 
        [Fact]
        public async Task UPDATE_MESSAGE_TO_SINGLE_USER_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetSingleUserChat()).Returns(MOCKRETURN_SINGLE_USER_CHATS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.UpdateMessageToSingleUserChat(MOCKRETURN_MESSAGE, 1));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.UpdateMessageToSingleUserChat(1, MOCKRETURN_MESSAGE) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task UPDATE_MESSAGE_TO_GENERAL_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetGeneralChat()).Returns(MOCKRETURN_GENERALCHATS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.UpdateMessageToGeneralChat(MOCKRETURN_MESSAGE, 1));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.UpdateMessageToGeneralChat(1, MOCKRETURN_MESSAGE) as OkObjectResult;
            Assert.NotNull(Return);
        }

        [Fact]
        public async Task ADD_MESSAGE_TO_GROUP_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetGroupChats()).Returns(MOCKRETURN_GROUPCHATS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.AddMessageToGroupChat(MOCKRETURN_MESSAGE, a => a.Id == 1));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.AddMessageToGroupChat(0, MOCKRETURN_MESSAGE) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task ADD_MESSAGE_TO_SINGLE_USER_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetSingleUserChat()).Returns(MOCKRETURN_SINGLE_USER_CHATS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.AddMessageToSingleUserChat(MOCKRETURN_MESSAGE, a => a.Id == 1));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.AddMessageToSingleUserChat(1, MOCKRETURN_MESSAGE) as OkObjectResult;
            Assert.NotNull(Return);
        }
        [Fact]
        public async Task ADD_MESSAGE_TO_GENERAL_CHAT()
        {
            LoggingPathSingleton PATH = LoggingPathSingleton.GetSingleton();
            PATH.SetToUnitTesting();
            var MockingRepository = new Mock<IRepository>();
            MockingRepository.Setup(a => a.GetGeneralChat()).Returns(MOCKRETURN_GENERALCHATS);
            MockingRepository.Setup(a => a.GetUserById(a => a.Id == 1)).Returns(MOCKRETURN_USER);
            MockingRepository.Setup(a => a.AddMessageToGeneralChat(MOCKRETURN_MESSAGE, a => a.Id == 1));
            _databaseSingleton.SetRepository(new MockingRepository());
            var chatService = new ChatService(_databaseSingleton);
            var controller = new ChatController(_databaseSingleton, chatService);
            var Return = await controller.AddMessageToGeneralChat(1, MOCKRETURN_MESSAGE) as OkObjectResult;
            Assert.NotNull(Return);
        }
    }
}
