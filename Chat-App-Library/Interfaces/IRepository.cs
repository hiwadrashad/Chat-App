using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IRepository
    {
#nullable enable
        public void UpdateRefreshToken(RefreshToken token);
        public List<RefreshToken> GetAllRefreshTokens();
        public Task AddRefreshToken(RefreshToken token);
        public void AddUser(User user);
        public void AddGroupChat(GroupChat groupChat);
        public void AddSingleUserChat(SingleUserChat singleUserChat);
        public void AddGeneralChat(GeneralChat generalChat);
        public List<User> GetUsers();
        public List<GroupChat> GetGroupChats();
        public List<GeneralChat> GetGeneralChat();
        public List<SingleUserChat> GetSingleUserChat();
        public List<Message> GetMessages();
        public List<Message> GetMessagesByUserId(Expression<Func<Message,bool>> id);
        public User? GetUserById(Expression<Func<User, bool>> id);
        public List<User> GetUserByName(Expression<Func<User, bool>> name);
        public List<GroupChat> GetGroupChatsByUserId(Expression<Func<GroupChat, bool>> id);
        public List<SingleUserChat> GetSingleUserChatByUserId(Expression<Func<SingleUserChat, bool>> id);
        public void UpdateUserData(User userupdate);
#nullable disable
        public void AddMessageToGroupChat(Message message, Expression<Func<GroupChat, bool>> id);
        public void AddMessageToSingleUserChat(Message message, Expression<Func<SingleUserChat, bool>> id);
        public void AddMessageToGeneralChat(Message message, Expression<Func<GeneralChat, bool>> id);
        public void DeleteUser(int id);
        public void DeleteGroup(GroupChat chat);
        public void DeleteGeneralChat(GeneralChat chat);
        public void DeleteSiglePersonChat(SingleUserChat chat);
        public void DeleteMessageGroup(GroupChat chat, int messageid);
        public void DeleteMessageGeneral(GeneralChat chat, int messageid);
        public void DeleteMessageSingleUser(SingleUserChat chat, int messageid);
        public void UpdateMessageToGroupChat(Message message, int groupid);
        public void UpdateMessageToSingleUserChat(Message message, int singleuserchatid);
        public void UpdateMessageToGeneralChat(Message message, int groupchatid);
        public void BlockUserFromGeneralChat(int UserId, GeneralChat Chat);
        public void BlockUserFromGroupChat(int UserId, GroupChat Chat);
        public void AddUserToGroupChat(int UserId, GroupChat Chat);
    }
}
