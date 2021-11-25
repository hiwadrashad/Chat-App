using Chat_App_Library.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Bussiness_Logic.Logging
{
    public class ApiLogging
    {
        public static void WriteErrorLog(string message)
        {
            LoggingPathSingleton IfUnitTesting = LoggingPathSingleton.GetSingleton();
            if (IfUnitTesting.GetIfCurrentlyUnitTesting() == false)
            {
                string fullpath = System.IO.Directory.GetCurrentDirectory();
                File.AppendAllText(fullpath + @"\Logs\" + "log.txt", message + Environment.NewLine);
            }
            else
            {
                string fullpath = System.IO.Directory.GetCurrentDirectory();
                File.AppendAllText(fullpath + @"\..\..\.." + @"\Logs\" + "log.txt", message + Environment.NewLine);
            }
        }
    }
}
