using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Singletons
{
    public class UserSingleton
    {
        private static User _currentuser;
        private UserSingleton()
        {

        }

        private static UserSingleton _userSingleton;

        public static UserSingleton GetSingleton()
        {
            if (_userSingleton == null)
            {
                _userSingleton = new UserSingleton();
            }
            return _userSingleton;
        }

        public void SetUser(User currentuser)
        {
            _currentuser = currentuser;
        }

        public User GetUser(User currentuser)
        {
            return _currentuser;
        }
    }
}
