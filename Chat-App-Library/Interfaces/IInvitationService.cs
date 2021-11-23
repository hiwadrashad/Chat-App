using Chat_App_Library.Enums;
using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IInvitationService
    {
        public Task<object> SendInvitation(int RecieverId, GroupType Grouptype, int Chatid, Invitation Invitation);
        public Task<object> AcceptInvitation(int RecieverId, Invitation invitation);
        public Task<object> DeclineInvitation(int RecieverId, Invitation invitation);
        public Task<object> GetGroup(Expression<Func<User, bool>> requestinguserid, string password, GroupType grouptype, int groupid);
    }
}
