using Chat_App_Library.Enums;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                Salt = "SALT",
                Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "string"

              },
              new User() {
                Email = "test",
                Id = 1,
                Name = "test",
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
                Username = "test"            
                
              },
              new User() {
                Email = "hiwad.rashad@itvitaelearning.nl",
                Id = 2,
                Name = "test",
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
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
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
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
               BannedUsers = new List<User>(),
               ChatBanned = false,
               MaxAmountPersons = 0,
               HashBase64 = "LynvGw5uWxVFi4bnuNqrWBByLwQFKoMF3XEEtIftGes=",
               Private = false,
             Users = new List<User>()
             {
               new User() {
                Email = "test",
                Id = 1,
                Name = "test",
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
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
               BannedUsers = new List<User>(),
               ChatBanned = false,
               Owner=
                          new User() {
                Email = "user@example.com",
                Id = 0,
                Name = "string",
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "string"

               },
               MaxAmountPersons = 0,
               Password = "password",
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
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
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
                OriginUser = new User()
               {
            Email = "test",
            Id = 1,
            Name = "test",
            Salt = "SALT",
            Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
            Banned = false,
            HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            Username = "test"
        },
        RecipientUser = new User()
        {
            Email = "test",
            Id = 2,
            Name = "test",
            Salt = "SALT",
            Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
            Banned = false,
            HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            Username = "test"
        },
        CreationDate = DateTime.Now,
                Id = 1,
                Title = "test",
                BannedUsers = new List<User>(),
                ChatBanned = false,
                MaxAmountPersons = 0,
                HashBase64 = "LynvGw5uWxVFi4bnuNqrWBByLwQFKoMF3XEEtIftGes=",
                Private = false,
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
                      Salt = "SALT",
                           Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                      Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
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
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
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

        public virtual List<Message> GetMessages()
        {
            return _messages;
        }

        public List<Message>GetMessagesByUserId(Expression<Func<Message,bool>> id)
        {
            IQueryable<Message> query = _messages.AsQueryable();
            return query.Where(id).ToList();
        }
#nullable enable
        public virtual User? GetUserById(Expression<Func<User, bool>> id)
        {
            IQueryable<User> query = _users.AsQueryable();
            return query.Where(id).FirstOrDefault();
        }

        public List<User> GetUserByName(Expression<Func<User,bool>> name)
        {
            IQueryable<User> query = _users.AsQueryable();
            return query.Where(name).ToList();
        }

        public List<GroupChat> GetGroupChatsByUserId(Expression<Func<GroupChat, bool>> id)
        {
            IQueryable<GroupChat> query = _groupchat.AsQueryable();
            return query.Where(id).ToList();
        }

        public List<SingleUserChat> GetSingleUserChatByUserId(Expression<Func<SingleUserChat, bool>> id)
        {
            IQueryable<SingleUserChat> query = _singleUserChats.AsQueryable();
            //return _singleUserChats.Where(a => a.OriginUser.Id == id || a.RecipientUser.Id == id).ToList();
            return query.Where(id).ToList();
        }

        public void UpdateUserData(User userupdate)
        {
            var user = _users.Where(a => a.Id == userupdate.Id).FirstOrDefault();
            if (!(user == null))
            {
                user.Email = userupdate.Email;
                user.Name = userupdate.Name;
                user.Salt = userupdate.Salt;
                user.Invitations = userupdate.Invitations;
                user.Banned = false;
                user.HashBase64 = userupdate.HashBase64;
                user.Role = userupdate.Role;
                user.Username = userupdate.Username;
            }
        }
#nullable disable

        public void UpdateMessageToGroupChat(Message message, int groupid)
        {
            var item = _groupchat.FirstOrDefault(a => a.Id == groupid)
            .Messages.Where(a => a.Id == message.Id).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _messages.FirstOrDefault(a => a.Id == message.Id);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
        }


        public void UpdateMessageToSingleUserChat(Message message, int singleuserchatid)
        {
            var item = _singleUserChats.FirstOrDefault(a => a.Id == singleuserchatid)
            .Messages.Where(a => a.Id == message.Id).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _messages.FirstOrDefault(a => a.Id == message.Id);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
        }

        public void UpdateMessageToGeneralChat(Message message, int groupchatid)
        {
            var item = _generalchat.FirstOrDefault(a => a.Id == groupchatid)
            .Messages.Where(a => a.Id == message.Id).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _messages.FirstOrDefault(a => a.Id == message.Id);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
        }

        public void AddMessageToGroupChat(Message message, Expression<Func<GroupChat, bool>> id)
        {
            IQueryable<GroupChat> query = _groupchat.AsQueryable();
            query.FirstOrDefault(id).Messages.Add(message);
            _messages.Add(message);
        }


        public void AddMessageToSingleUserChat(Message message, Expression<Func<SingleUserChat, bool>> id)
        {
            IQueryable<SingleUserChat> query = _singleUserChats.AsQueryable();
            query.FirstOrDefault(id).Messages.Add(message);
            _messages.Add(message);
        }

        public void AddMessageToGeneralChat(Message message, Expression<Func<GeneralChat, bool>> id)
        {
            IQueryable<GeneralChat> query = _generalchat.AsQueryable();
            query.FirstOrDefault().Messages.Add(message);
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

        public void DeleteMessageGeneral(GeneralChat chat, int messageid)
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

        /// <summary>
        /// V1.2
        /// </summary>
   
        public void BlockUserFromGeneralChat(int UserId, GeneralChat Chat)
        {
            var User = _users.Where(a => a.Id == UserId).FirstOrDefault();
            var ChatToUpdate = _generalchat.Where(a => a.Id == Chat.Id).FirstOrDefault();
            ChatToUpdate.BannedUsers = Chat.BannedUsers;
            ChatToUpdate.BannedUsers.Add(User);
            ChatToUpdate.ChatBanned = Chat.ChatBanned;
            ChatToUpdate.CreationDate = Chat.CreationDate;
            ChatToUpdate.Id = Chat.Id;
            ChatToUpdate.MaxAmountPersons = Chat.MaxAmountPersons;
            ChatToUpdate.Messages = Chat.Messages;
            ChatToUpdate.Owner = Chat.Owner;
            ChatToUpdate.Password = Chat.Password;
            ChatToUpdate.Private = Chat.Private;
            ChatToUpdate.Title = Chat.Title;
        }

        public void BlockUserFromGroupChat(int UserId, GroupChat Chat)
        {
            var User = _users.Where(a => a.Id == UserId).FirstOrDefault();
            var ChatToUpdate = _groupchat.Where(a => a.Id == Chat.Id).FirstOrDefault();
            ChatToUpdate.BannedUsers = Chat.BannedUsers;
            ChatToUpdate.BannedUsers.Add(User);
            ChatToUpdate.ChatBanned = Chat.ChatBanned;
            ChatToUpdate.CreationDate = Chat.CreationDate;
            ChatToUpdate.Id = Chat.Id;
            ChatToUpdate.MaxAmountPersons = Chat.MaxAmountPersons;
            ChatToUpdate.Messages = Chat.Messages;
            ChatToUpdate.GroupOwner = Chat.GroupOwner;
            ChatToUpdate.HashBase64 = Chat.HashBase64;
            ChatToUpdate.Private = Chat.Private;
            ChatToUpdate.Title = Chat.Title;
        }

        public void AddUserToGroupChat(int UserId, GroupChat Chat)
        {
            var User = _users.Where(a => a.Id == UserId).FirstOrDefault();
            var ChatToUpdate = _groupchat.Where(a => a.Id == Chat.Id).FirstOrDefault();
            ChatToUpdate.Users.Add(User);
        }

        public void SeedMoqData()
        {
            throw new NotImplementedException();
        }
    }
}
