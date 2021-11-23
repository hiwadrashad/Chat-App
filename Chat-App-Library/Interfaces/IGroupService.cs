using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IGroupService
    {
        public Task<object> AddGroupChat(string password, GroupChat input);
        public Task<object> AddSingleUserChat(string password, SingleUserChat input);
        public Task<object> AddGeneralChat(GeneralChat input);
        public Task<object> DeleteGroup(int requestuserid, GroupChat input);
        public Task<object> DeleteGeneralChat(int requestuserid, GeneralChat input);
        public Task<object> DeleteSinglePersonChat(int requestuserid, SingleUserChat input);
        public Task<object> GetGroupChatsByUserId(int requestuserid);
        public Task<object> GetSingleUserChatByUserId(int requestuserid);
        public Task<object> GetGroupChats(int requestuserid);
        public Task<object> GetGeneralChat(int requestuserid);
        public Task<object> GetSingleUserChat(int requestuserid);
    }
}
