using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IRepository
    {
#nullable enable
        public void AddUser(User user);
        public void AddGroupChat(GroupChat groupChat);
        public void AddSingleUserChat(SingleUserChat singleUserChat);
        public void AddGeneralChat(GeneralChat generalChat);
        public List<User> GetUsers();
        public List<GroupChat> GetGroupChats();
        public List<GeneralChat> GetGeneralChat();
        public List<SingleUserChat> GetSingleUserChat();
        public List<Message> GetMessages();
        public List<Message> GetMessagesByUserId(int id);
        public User? GetUserById(int id);
        public List<User> GetUserByName(string name);
        public List<GroupChat> GetGroupChatsByUserId(int id);
        public List<SingleUserChat> GetSingleUserChatByUserId(int id);
        public void UpdateUserData(User userupdate);
#nullable disable
        public void AddMessageToGroupChat(Message message, int groupid);
        public void AddMessageToSingleUserChat(Message message, int singleuserchatid);
        public void AddMessageToGeneralChat(Message message, int groupchatid);
        public void DeleteUser(int id);
        public void DeleteGroup(GroupChat chat);
        public void DeleteGeneralChat(GeneralChat chat);
        public void DeleteSiglePersonChat(SingleUserChat chat);
        public void DeleteMessageGroup(GroupChat chat, int messageid);
        public void DeleteMessageGeneral(GroupChat chat, int messageid);
        public void DeleteMessageSingleUser(SingleUserChat chat, int messageid);
        public void UpdateMessageToGroupChat(Message message, int groupid);
        public void UpdateMessageToSingleUserChat(Message message, int singleuserchatid);
        public void UpdateMessageToGeneralChat(Message message, int groupchatid);
    }
}
