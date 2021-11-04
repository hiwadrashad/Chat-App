using Chat_App_Database.Mocks;
using Chat_App_Library.Models;
using Chat_App_Library.Singletons;
using System;

namespace Chat_App_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            User someuser = new User();
            DatabaseSingleton.GetSingleton().SetRepository(new MockingRepository());
            var repository = DatabaseSingleton.GetSingleton().GetRepository();
            repository.AddUser(someuser);
            Console.WriteLine("Hello World!");
        }
    }
}
