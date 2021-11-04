using Chat_App_Database.Mocks;
using Chat_App_Library.Singletons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Console.DependencyInjection
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
