using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Logic.Mocks
{
    public class MockingRepository : IRepository
    {
        private static List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        private static List<User> _users = new List<User>()
            {         new User() {
                Email = "user@example.com",
                Id = 0,
                Name = "string",
                Password = "string",
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "string"

              },
              new User() {
                Email = "test",
                Id = 1,
                Name = "test",
                Password = "test",
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"            
                
              }
            };
        private static List<GroupChat> _groupchat = new List<GroupChat>()
            {
             new GroupChat()
             {
                CreationDate = DateTime.Now,
                Id = 1,
                Title = "test",
                GroupOwner =        new User() {
                Email = "test",
                Id = 1,
                Name = "test",
                Password = "test",
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"

              },
                Messages = new List<Message>()
             {
                new Message()
             {
                 EndDate = DateTime.Now,
                 Id = 1,
                 StartDate = DateTime.Now,
                 Text = "test",
             }
             },
             Users = new List<User>()
             {
               new User() {
                Email = "test",
                Id = 1,
                Name = "test",
                Password = "test",
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"

              }
             }
             
             }
            };
        private static List<GeneralChat> _generalchat = new List<GeneralChat>()
            {
              new GeneralChat()
              {
               CreationDate = DateTime.Now,
               Id = 1,
               Title = "test",
               Messages = new List<Message>()
               {
                       new Message()
               {
                EndDate = DateTime.Now,
                Id = 1,
                StartDate = DateTime.Now,
                Text = "test",
                User =    new User() {
                Email = "test",
                Id = 1,
                Name = "test",
                Password = "test",
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"

              }
               }
               }
              
              }
            };
        private static List<SingleUserChat> _singleUserChats = new List<SingleUserChat>()
            {
              new SingleUserChat()
              {
                CreationDate = DateTime.Now,
                Id = 1,
                Title = "test",
                Messages = new List<Message>()
                {
                     new Message()
                     {
                      EndDate = DateTime.Now,
                      Id = 1,
                      StartDate = DateTime.Now,
                      Text = "test",
                      User =    new User() {
                      Email = "test",
                      Id = 1,
                      Name = "test",
                      Password = "test",
                      Role = Chat_App_Library.Enums.Role.Admin,
                      Username = "test"

                     }
                }

                }
              }
            };
        private static List<Message> _messages = new List<Message>()
            {
             new Message()
             {
                EndDate = DateTime.Now,
                Id = 1,
                StartDate = DateTime.Now,
                Text = "test",
                User =  new User() {
                Email = "test",
                Id = 1,
                Name = "test",
                Password = "test",
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"

              }
             }
            };
        private static MockingRepository _mockingRepository;

        private static MockingRepository GetMockingRepository()
        {
            if (_mockingRepository == null)
            {
                _mockingRepository = new MockingRepository();
            }
            return _mockingRepository;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _refreshTokens;
        }

        public void UpdateRefreshToken(RefreshToken token)
        {
            var foundtoken = _refreshTokens.Where(a => a.Id == token.Id).FirstOrDefault();
            foundtoken.AddedDate = token.AddedDate;
            foundtoken.ExpiryDate = token.ExpiryDate;
            foundtoken.Id = token.Id;
            foundtoken.IsRevoked = token.IsRevoked;
            foundtoken.IsUsed = token.IsUsed;
            foundtoken.jwtId = token.jwtId;
            foundtoken.Token = token.Token;
            foundtoken.User = token.User;
            foundtoken.UserId = token.UserId;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task AddRefreshToken(RefreshToken token)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            _refreshTokens.Add(token);
        }
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void AddGroupChat(GroupChat groupChat)
        {
            _groupchat.Add(groupChat);
        }

        public void AddSingleUserChat(SingleUserChat singleUserChat)
        {
            _singleUserChats.Add(singleUserChat);
        }

        public void AddGeneralChat(GeneralChat generalChat)
        {
            _generalchat.Add(generalChat);
        }


        public List<User> GetUsers()
        {
            return _users;
        }

        public List<GroupChat> GetGroupChats()
        {
            return _groupchat;
        }

        public List<GeneralChat> GetGeneralChat()
        {
            return _generalchat;
        }

        public List<SingleUserChat> GetSingleUserChat()
        {
            return _singleUserChats;
        }

        public List<Message> GetMessages()
        {
            return _messages;
        }

        public List<Message> GetMessagesByUserId(int id)
        {
            return _messages.Where(a => a.User.Id == id).ToList();
        }
#nullable enable
        public User? GetUserById(int id)
        {
            return _users.Where(a => a.Id == id).FirstOrDefault();
        }

        public List<User> GetUserByName(string name)
        {
            return _users.Where(a => a.Name == name).ToList();
        }

        public List<GroupChat> GetGroupChatsByUserId(int id)
        {
            return _groupchat.Where(a => a.Users.All(a => a.Id == id) || a.GroupOwner.Id == id).ToList();
        }

        public List<SingleUserChat> GetSingleUserChatByUserId(int id)
        {
            return _singleUserChats.Where(a => a.OriginUser.Id == id || a.RecipientUser.Id == id).ToList();
        }

        public void UpdateUserData(User userupdate)
        {
            var user = _users.Where(a => a.Id == userupdate.Id).FirstOrDefault();
            if (!(user == null))
            {
                user.Email = userupdate.Email;
                user.Name = userupdate.Name;
                user.Password = userupdate.Password;
                user.Role = userupdate.Role;
                user.Username = userupdate.Username;
            }
        }
#nullable disable

        public void UpdateMessageToGroupChat(Message message, int groupid)
        {
            var item = _groupchat.FirstOrDefault(a => a.Id == groupid)
            .Messages.Where(a => a.Id == groupid).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _messages.FirstOrDefault(a => a.Id == groupid);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
        }


        public void UpdateMessageToSingleUserChat(Message message, int singleuserchatid)
        {
            var item = _singleUserChats.FirstOrDefault(a => a.Id == singleuserchatid)
            .Messages.Where(a => a.Id == singleuserchatid).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _messages.FirstOrDefault(a => a.Id == singleuserchatid);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
        }

        public void UpdateMessageToGeneralChat(Message message, int groupchatid)
        {
            var item = _generalchat.FirstOrDefault(a => a.Id == groupchatid)
            .Messages.Where(a => a.Id == groupchatid).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _messages.FirstOrDefault(a => a.Id == groupchatid);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
        }

        public void AddMessageToGroupChat(Message message, int groupid)
        {
            _groupchat.FirstOrDefault(a => a.Id == groupid).Messages.Add(message);
            _messages.Add(message);
        }


        public void AddMessageToSingleUserChat(Message message, int singleuserchatid)
        {
            _singleUserChats.FirstOrDefault(a => a.Id == singleuserchatid).Messages.Add(message);
            _messages.Add(message);
        }

        public void AddMessageToGeneralChat(Message message, int groupchatid)
        {
            _generalchat.FirstOrDefault(a => a.Id == groupchatid).Messages.Add(message);
            _messages.Add(message);
        }

        public void DeleteUser(int id)
        {
            _users.Remove(_users.Where(a => a.Id == id).FirstOrDefault());
        }

        public void DeleteGroup(GroupChat chat)
        {
            _groupchat.Remove(chat);
        }

        public void DeleteGeneralChat(GeneralChat chat)
        {
            _generalchat.Remove(chat);
        }

        public void DeleteSiglePersonChat(SingleUserChat chat)
        {
            _singleUserChats.Remove(chat);
        }

        public void DeleteMessageGroup(GroupChat chat, int messageid)
        {
            _groupchat.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Remove(_groupchat.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Where(a => a.Id == messageid).FirstOrDefault());
        }

        public void DeleteMessageGeneral(GroupChat chat, int messageid)
        {
            _generalchat.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Remove(_generalchat.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Where(a => a.Id == messageid).FirstOrDefault());
        }

        public void DeleteMessageSingleUser(SingleUserChat chat, int messageid)
        {
            _singleUserChats.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Remove(_singleUserChats.Where(a => a.Id == chat.Id).FirstOrDefault().
            Messages.Where(a => a.Id == messageid).FirstOrDefault());
        }
    }
}
