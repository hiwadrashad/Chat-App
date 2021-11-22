using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.ViewModels
{
    public class CollectiveGroup
    {
        public GeneralChat GeneralChat { get; set; }
        public GroupChat GroupChat { get; set; }
        public SingleUserChat SingleUserChat { get; set; }
    }
}
