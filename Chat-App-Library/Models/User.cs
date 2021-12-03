using Chat_App_Library.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashBase64 { get; set; }
        public Role Role { get; set; }
        public bool Banned { get; set; }
        public string InvitationsId { get; set; }
        [ForeignKey("InvitationsId")]
        public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
