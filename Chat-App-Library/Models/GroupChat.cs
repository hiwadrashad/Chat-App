using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Models
{
    public class GroupChat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string MessagesId { get; set; }
        [ForeignKey("MessagesId")]
        public List<Message> Messages { get; set; } = new List<Message>();
        public string UsersId { get; set; }
        [ForeignKey("UsersId")]
        public List<User> Users { get; set; }
        public int GroupOwnerId { get; set; }
        [ForeignKey("GroupOwnerId")]
        public User GroupOwner { get; set; }
        public bool ChatBanned { get; set; }
        public int MaxAmountPersons { get; set; }
        public string HashBase64 { get; set; }
        public bool Private { get; set; }
        public string BannedUsersId { get; set; }
        [ForeignKey("BannedUsersId")]
        public List<User> BannedUsers { get; set; } = new List<User>();

    }
}
