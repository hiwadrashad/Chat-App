using Chat_App_Bussiness_Logic.Configuration;
using Chat_App_Bussiness_Logic.Services;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Singletons;
using Chat_App_Logic.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Bussiness_Logic.DependencyInjection
{
    public class Startup
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                services.AddSingleton(typeof(IDatabaseSingleton), DatabaseSingleton.GetSingleton());
                services.AddSingleton(typeof(IChatService), new ChatService(DatabaseSingleton.GetSingleton()));
                DatabaseSingleton.GetSingleton().SetRepository(new MockingRepository());
            });

        }


    }
}
