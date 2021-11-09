using Chat_App_Database.Database;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Logic.Repositories
{
    public class ChatDbContextRepository : IRepository
    {
        private ChatDbContext _dbContext;

        private static ChatDbContextRepository _instance;

        public ChatDbContextRepository()
        {
            _dbContext = new ChatDbContext();
        }

        public static ChatDbContextRepository GetSingleton()
        {
            if (_instance == null)
            {
                _instance = new ChatDbContextRepository();
            }
            return _instance;
        }

        public void ClearAllDataSets()
        {
            foreach (var item in _dbContext.GeneralChatDatabase)
            {
                _dbContext.GeneralChatDatabase.Remove(item);
            }
            foreach (var item in _dbContext.GroupChatDatabase)
            {
                _dbContext.GroupChatDatabase.Remove(item);
            }
            foreach (var item in _dbContext.MessageDatabase)
            {
                _dbContext.MessageDatabase.Remove(item);
            }
            foreach (var item in _dbContext.SingleUserChatDatabase)
            {
                _dbContext.SingleUserChatDatabase.Remove(item);
            }
            foreach (var item in _dbContext.UserDatabase)
            {
                _dbContext.UserDatabase.Remove(item);
            }
        }
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _dbContext.RefreshTokens.ToList();
        }
        public void UpdateRefreshToken(RefreshToken token)
        {
            var foundtoken = _dbContext.RefreshTokens.Where(a => a.Id == token.Id).FirstOrDefault();
            foundtoken.AddedDate = token.AddedDate;
            foundtoken.ExpiryDate = token.ExpiryDate;
            foundtoken.Id = token.Id;
            foundtoken.IsRevoked = token.IsRevoked;
            foundtoken.IsUsed = token.IsUsed;
            foundtoken.jwtId = token.jwtId;
            foundtoken.Token = token.Token;
            foundtoken.User = token.User;
            foundtoken.UserId = token.UserId;
            _dbContext.SaveChanges();
        }
        public async Task AddRefreshToken(RefreshToken token)
        {
            await _dbContext.RefreshTokens.AddAsync(token);
            _dbContext.SaveChanges();
        }

        public void AddUser(User user)
        {
            _dbContext.UserDatabase.Add(user);
            _dbContext.SaveChanges();
        }

        public void AddGroupChat(GroupChat groupChat)
        {
            _dbContext.GroupChatDatabase.Add(groupChat);
            _dbContext.SaveChanges();
        }

        public void AddSingleUserChat(SingleUserChat singleUserChat)
        {
            _dbContext.SingleUserChatDatabase.Add(singleUserChat);
            _dbContext.SaveChanges();
        }

        public void AddGeneralChat(GeneralChat generalChat)
        {
            
            _dbContext.GeneralChatDatabase.Add(generalChat);
            _dbContext.SaveChanges();
        }


        public List<User> GetUsers()
        {
            return _dbContext.UserDatabase.ToList();
        }

        public List<GroupChat> GetGroupChats()
        {
            return _dbContext.GroupChatDatabase.ToList();
        }

        public List<GeneralChat> GetGeneralChat()
        {
            return _dbContext.GeneralChatDatabase.ToList();
        }

        public List<SingleUserChat> GetSingleUserChat()
        {
            return _dbContext.SingleUserChatDatabase.ToList();
        }

        public List<Message> GetMessages()
        {
            return _dbContext.MessageDatabase.ToList();
        }

        public List<Message> GetMessagesByUserId(int id)
        {
            return _dbContext.MessageDatabase.Where(a => a.User.Id == id).ToList();
        }
#nullable enable
        public User? GetUserById(int id)
        {
            return _dbContext.UserDatabase.Where(a => a.Id == id).FirstOrDefault();
        }

        public List<User> GetUserByName(string name)
        {
            return _dbContext.UserDatabase.Where(a => a.Name == name).ToList();
        }

        public List<GroupChat> GetGroupChatsByUserId(int id)
        {
            return _dbContext.GroupChatDatabase.Where(a => a.Users.All(a => a.Id == id) ||
            a.GroupOwner.Id == id).ToList();
        }

        public List<SingleUserChat> GetSingleUserChatByUserId(int id)
        {
            return _dbContext.SingleUserChatDatabase.Where(a => a.OriginUser.Id == id ||
            a.RecipientUser.Id == id).ToList();
        }

        public void UpdateUserData(User userupdate)
        {
            var user = _dbContext.UserDatabase.Where(a => a.Id == userupdate.Id).FirstOrDefault();
            if (!(user == null))
            {
                user.Email = userupdate.Email;
                user.Name = userupdate.Name;
                user.Password = userupdate.Password;
                user.Role = userupdate.Role;
                user.Username = userupdate.Username;
            }
            _dbContext.SaveChanges();
        }
#nullable disable

        public void AddMessageToGroupChat(Message message, int groupid)
        {
            _dbContext.GroupChatDatabase.FirstOrDefault(a => a.Id == groupid).Messages.Add(message);
            _dbContext.MessageDatabase.Add(message);
            _dbContext.SaveChanges();
        }


        public void AddMessageToSingleUserChat(Message message, int singleuserchatid)
        {
            _dbContext.SingleUserChatDatabase.FirstOrDefault(a => a.Id == singleuserchatid)
            .Messages.Add(message);
            _dbContext.MessageDatabase.Add(message);
            _dbContext.SaveChanges();
        }

        public void AddMessageToGeneralChat(Message message, int groupchatid)
        {
            _dbContext.GeneralChatDatabase.FirstOrDefault(a => a.Id == groupchatid).Messages.Add(message);
            _dbContext.MessageDatabase.Add(message);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _dbContext.UserDatabase.Remove(_dbContext.UserDatabase.Where(a => a.Id == id).FirstOrDefault());
            _dbContext.SaveChanges();
        }

        public void DeleteGroup(GroupChat chat)
        {
            _dbContext.GroupChatDatabase.Remove(chat);
            _dbContext.SaveChanges();
        }

        public void DeleteGeneralChat(GeneralChat chat)
        {
            _dbContext.GeneralChatDatabase.Remove(chat);
            _dbContext.SaveChanges();
        }

        public void DeleteSiglePersonChat(SingleUserChat chat)
        {
            _dbContext.SingleUserChatDatabase.Remove(chat);
            _dbContext.SaveChanges();
        }

        public void DeleteMessageGroup(GroupChat chat, int messageid)
        {
            _dbContext.GroupChatDatabase.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Remove(_dbContext.GroupChatDatabase.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Where(a => a.Id == messageid).FirstOrDefault());
            _dbContext.SaveChanges();
        }

        public void DeleteMessageGeneral(GroupChat chat, int messageid)
        {
            _dbContext.GeneralChatDatabase.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Remove(_dbContext.GeneralChatDatabase.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Where(a => a.Id == messageid).FirstOrDefault());
            _dbContext.SaveChanges();
        }

        public void DeleteMessageSingleUser(SingleUserChat chat, int messageid)
        {
            _dbContext.SingleUserChatDatabase.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Remove(_dbContext.SingleUserChatDatabase.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Where(a => a.Id == messageid).FirstOrDefault());
            _dbContext.SaveChanges();
        }

        public void UpdateMessageToGroupChat(Message message, int groupid)
        {
            var item = _dbContext.GroupChatDatabase.FirstOrDefault(a => a.Id == groupid)
            .Messages.Where(a => a.Id == groupid).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _dbContext.MessageDatabase.FirstOrDefault(a => a.Id == groupid);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
            _dbContext.SaveChanges();
        }


        public void UpdateMessageToSingleUserChat(Message message, int singleuserchatid)
        {
            var item = _dbContext.SingleUserChatDatabase.FirstOrDefault(a => a.Id == singleuserchatid)
            .Messages.Where(a => a.Id == singleuserchatid).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _dbContext.MessageDatabase.FirstOrDefault(a => a.Id == singleuserchatid);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
            _dbContext.SaveChanges();
        }

        public void UpdateMessageToGeneralChat(Message message, int groupchatid)
        {
            var item = _dbContext.GeneralChatDatabase.FirstOrDefault(a => a.Id == groupchatid)
            .Messages.Where(a => a.Id == groupchatid).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _dbContext.MessageDatabase.FirstOrDefault(a => a.Id == groupchatid);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
            _dbContext.SaveChanges();
        }
    }
}
