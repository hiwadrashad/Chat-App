using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Models
{
    public class SingleUserChat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public User OriginUser { get; set; }
        public User RecipientUser { get; set; }
        public bool ChatBanned { get; set; }
        public int MaxAmountPersons { get; set; }
        public string HashBase64 { get; set; }
        public bool Private { get; set; }
        public List<User> BannedUsers { get; set; } = new List<User>();
    }
}
