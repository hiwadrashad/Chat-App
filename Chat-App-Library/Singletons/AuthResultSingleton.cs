using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Singletons
{
    public class AuthResultSingleton
    {
        private static AuthResult _currentuser;
        private AuthResultSingleton()
        {

        }

        private static AuthResultSingleton _userSingleton;

        public static AuthResultSingleton GetSingleton()
        {
            if (_userSingleton == null)
            {
                _userSingleton = new AuthResultSingleton();
            }
            return _userSingleton;
        }

        public void SetAuth(AuthResult currentuser)
        {
            _currentuser = currentuser;
        }

        public AuthResult GetAuth()
        {
            return _currentuser;
        }
    }
}
