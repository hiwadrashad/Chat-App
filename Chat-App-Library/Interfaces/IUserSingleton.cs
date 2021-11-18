using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IUserSingleton
    {
        public void SetUser(User currentuser);
        public User GetUser(User currentuser);
    }
}
