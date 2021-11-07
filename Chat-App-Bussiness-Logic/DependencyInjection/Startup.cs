using Chat_App_Library.Singletons;
using Chat_App_Logic.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                DatabaseSingleton.GetSingleton().SetRepository(new MockingRepository());
            });
        }
    }
}
