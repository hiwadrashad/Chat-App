using Chat_App_Library.Enums;
using Chat_App_Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Models
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateSend { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
        public bool Accepted { get; set; }
        public GroupType Grouptype {get;set;}
        public int GroupId { get; set; }
    }
}
