using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Models
{
    public class GeneralChat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public int? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
        public bool ChatBanned { get; set; }
        public int MaxAmountPersons { get; set; }
        public string Password { get; set; }
        public bool Private { get; set; }
        public List<User> BannedUsers { get; set; } = new List<User>();
    }
}
