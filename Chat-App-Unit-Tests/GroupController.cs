﻿using Castle.Core.Configuration;
using Chat_App_Bussiness_Logic.Configuration;
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
    public class GroupController : MockData
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IDatabaseSingleton _databaseSingleton;
        private readonly IRepository _repo;
        private readonly IGroupService _groupService;
        private readonly ICredentialsService _credentialsService;
        public GroupController()
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
        public async void ADD_GROUP_CHAT()
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
    }
}
