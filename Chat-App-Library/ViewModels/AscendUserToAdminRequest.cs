using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.ViewModels
{
    public class AscendUserToAdminRequest
    {
        public User RequestingUser { get; set; }
        public User UserToAscend { get; set; }
    }
}
