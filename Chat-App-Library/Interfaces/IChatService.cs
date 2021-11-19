using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IChatService
    {
        public Task<object> GetMessages(Expression<Func<User, bool>> id);
        public  Task<object> GetMessagesByUserId(Expression<Func<Message, bool>> id);
        public  Task<object> DeleteMessageGroup(int id, GroupChat input);
        public  Task<object> DeleteMessageGeneral(int id, GroupChat input);
        public  Task<object> DeleteMessageSingleUser(int id, SingleUserChat input);
        public  Task<object> UpdateMessageToGroupChat(int id, Message input);
        public  Task<object> UpdateMessageToSingleUserChat(int id, Message input);
        public Task<object> UpdateMessageToGeneralChat(int id, Message input);
        public Task<object> AddMessageToGroupChat(Expression<Func<GroupChat, bool>> id, Message input);
        public Task<object> AddMessageToSingleUserChat(Expression<Func<SingleUserChat, bool>> id, Message input);
        public Task<object> AddMessageToGeneralChat(Expression<Func<GeneralChat, bool>> id, Message input);
    }
}
