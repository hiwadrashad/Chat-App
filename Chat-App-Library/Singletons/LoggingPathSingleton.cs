using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Singletons
{
    public class LoggingPathSingleton
    {

        private static bool _currentlyunittesting;
        private LoggingPathSingleton()
        {

        }

        private static LoggingPathSingleton _loggingPathSingleton;

        public static LoggingPathSingleton GetSingleton()
        {
            if (_loggingPathSingleton == null)
            {
                _loggingPathSingleton = new LoggingPathSingleton();
            }
            return _loggingPathSingleton;
        }

        public void SetToUnitTesting()
        {
            _currentlyunittesting = true;
        }

        public bool GetIfCurrentlyUnitTesting()
        {
            return _currentlyunittesting;
        }
    }
}
