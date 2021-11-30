using Chat_App_Database.Database;
using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            _dbContext.Database.ExecuteSqlRaw("DELETE FROM [Invitation]");
            _dbContext.UserDatabase.RemoveRange(_dbContext.UserDatabase);
            _dbContext.SingleUserChatDatabase.RemoveRange(_dbContext.SingleUserChatDatabase);
            _dbContext.MessageDatabase.RemoveRange(_dbContext.MessageDatabase);
            _dbContext.GroupChatDatabase.RemoveRange(_dbContext.GroupChatDatabase);
            _dbContext.GeneralChatDatabase.RemoveRange(_dbContext.GeneralChatDatabase);
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('GeneralChatDatabase',RESEED,0);");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('GroupChatDatabase',RESEED,0);");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Invitation',RESEED,0);");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('MessageDatabase',RESEED,0);");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('RefreshTokens',RESEED,0);");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('SingleUserChatDatabase',RESEED,0);");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('UserDatabase',RESEED,0);");
            _dbContext.SaveChanges();
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

        public virtual List<Message> GetMessages()
        {
            return _dbContext.MessageDatabase.ToList();
        }

        public List<Message> GetMessagesByUserId(Expression<Func<Message, bool>> id)
        {
            IQueryable<Message> query = _dbContext.MessageDatabase as IQueryable<Message>;
            return query.Where(id).ToList();
        }
#nullable enable
        public virtual User? GetUserById(Expression<Func<User, bool>> id)
        {
            IQueryable<User> query = _dbContext.UserDatabase.AsQueryable();
            return query.Where(id).FirstOrDefault();
        }

        public List<User> GetUserByName(Expression<Func<User, bool>> name)
        {
            IQueryable<User> query = _dbContext.UserDatabase.AsQueryable();
            return query.Where(name).ToList();
        }

        public List<GroupChat> GetGroupChatsByUserId(Expression<Func<GroupChat, bool>> id)
        {
            IQueryable<GroupChat> query = _dbContext.GroupChatDatabase.AsQueryable();
            return query.Where(id).ToList();
        }

        public List<SingleUserChat> GetSingleUserChatByUserId(Expression<Func<SingleUserChat, bool>> id)
        {
            IQueryable<SingleUserChat> query = _dbContext.SingleUserChatDatabase.AsQueryable();
            return query.Where(id).ToList();
        }

        public void UpdateUserData(User userupdate)
        {
            var user = _dbContext.UserDatabase.Where(a => a.Id == userupdate.Id).FirstOrDefault();
            if (!(user == null))
            {
                user.Email = userupdate.Email;
                user.Name = userupdate.Name;
                user.HashBase64 = userupdate.HashBase64;
                user.Salt = userupdate.Salt;
                user.Invitations = userupdate.Invitations;
                user.Role = userupdate.Role;
                user.Username = userupdate.Username;
            }
            _dbContext.SaveChanges();
        }
#nullable disable

        public void AddMessageToGroupChat(Message message, Expression<Func<GroupChat, bool>> id)
        {
            IQueryable<GroupChat> query = _dbContext.GroupChatDatabase.AsQueryable();
            query.FirstOrDefault(id).Messages.Add(message);
            _dbContext.MessageDatabase.Add(message);
            _dbContext.SaveChanges();
        }


        public void AddMessageToSingleUserChat(Message message, Expression<Func<SingleUserChat, bool>> id)
        {
            IQueryable<SingleUserChat> query = _dbContext.SingleUserChatDatabase.AsQueryable();
            query.FirstOrDefault(id)
            .Messages.Add(message);
            _dbContext.MessageDatabase.Add(message);
            _dbContext.SaveChanges();
        }

        public void AddMessageToGeneralChat(Message message, Expression<Func<GeneralChat, bool>> id)
        {
            IQueryable<GeneralChat> query = _dbContext.GeneralChatDatabase.AsQueryable();
            query.FirstOrDefault(id).Messages.Add(message);
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

        public void DeleteMessageGeneral(GeneralChat chat, int messageid)
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
            .Messages.Where(a => a.Id == message.Id).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _dbContext.MessageDatabase.FirstOrDefault(a => a.Id == message.Id);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
            _dbContext.SaveChanges();
        }


        public void UpdateMessageToSingleUserChat(Message message, int singleuserchatid)
        {
            var item = _dbContext.SingleUserChatDatabase.FirstOrDefault(a => a.Id == singleuserchatid)
            .Messages.Where(a => a.Id == message.Id).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _dbContext.MessageDatabase.FirstOrDefault(a => a.Id == message.Id);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
            _dbContext.SaveChanges();
        }

        public void UpdateMessageToGeneralChat(Message message, int groupchatid)
        {
            var item = _dbContext.GeneralChatDatabase.FirstOrDefault(a => a.Id == groupchatid)
            .Messages.Where(a => a.Id == message.Id).FirstOrDefault();
            item.EndDate = DateTime.Now;
            item.Text = message.Text;
            var item2 = _dbContext.MessageDatabase.FirstOrDefault(a => a.Id == message.Id);
            item2.EndDate = DateTime.Now;
            item2.Text = message.Text;
            _dbContext.SaveChanges();
        }

        public void BlockUserFromGeneralChat(int UserId, GeneralChat Chat)
        {
            var User = _dbContext.UserDatabase.Where(a => a.Id == UserId).FirstOrDefault();
            var ChatToUpdate = _dbContext.GeneralChatDatabase.Where(a => a.Id == Chat.Id).FirstOrDefault();
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
            _dbContext.SaveChanges();
        }

        public void BlockUserFromGroupChat(int UserId, GroupChat Chat)
        {
            var User = _dbContext.UserDatabase.Where(a => a.Id == UserId).FirstOrDefault();
            var ChatToUpdate = _dbContext.GroupChatDatabase.Where(a => a.Id == Chat.Id).FirstOrDefault();
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
            _dbContext.SaveChanges();
        }

        public void AddUserToGroupChat(int UserId, GroupChat Chat)
        {
            var User = _dbContext.UserDatabase.Where(a => a.Id == UserId).FirstOrDefault();
            var ChatToUpdate = _dbContext.GroupChatDatabase.Where(a => a.Id == Chat.Id).FirstOrDefault();
            ChatToUpdate.Users.Add(User);
            _dbContext.SaveChanges();
        }

        public void SeedMoqData()
        {
            _dbContext.UserDatabase.AddRange(new List<User>()
            {         new User() {
                Email = "user@example.com",
                //Id = 0,
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
                //Id = 1,
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
                //Id = 2,
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
            });
            _dbContext.MessageDatabase.AddRange(new List<Message>()
            {
             new Message()
             {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Text = "test",
                User =  new User() {
                Email = "test",
                //Id = 1,
                Name = "test",
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    //Id = 0,
                    Message = "Test"
                   }
                },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password","SALT")),
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"

              }
             }
            });
            _dbContext.SingleUserChatDatabase.AddRange(new List<SingleUserChat>()
                {
                  new SingleUserChat()
                  {

                    OriginUser = new User()
                   {
                Email = "test",
                //Id = 1,
                Name = "test",
                Salt = "SALT",
                Invitations = new List<Invitation>()
                    {
                       new Invitation()
                       {
                        Accepted = false,
                        Seen = false,
                        DateSend = DateTime.Now,
                        //Id = 0,
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
                //Id = 2,
                Name = "test",
                Salt = "SALT",
                Invitations = new List<Invitation>()
                    {
                       new Invitation()
                       {
                        Accepted = false,
                        Seen = false,
                        DateSend = DateTime.Now,
                        //Id = 0,
                        Message = "Test"
                       }
                    },
                Banned = false,
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password", "SALT")),
                Username = "test"
            },
            CreationDate = DateTime.Now,
                    //Id = 1,
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
                          //Id = 1,
                          StartDate = DateTime.Now,
                          Text = "test",
                          User =    new User() {
                          Email = "test",
                          //Id = 1,
                          Name = "test",
                          Salt = "SALT",
                               Invitations = new List<Invitation>()
                    {
                       new Invitation()
                       {
                        Accepted = false,
                        Seen = false,
                        DateSend = DateTime.Now,
                        //Id = 0,
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
                });
            _dbContext.GeneralChatDatabase.AddRange(new List<GeneralChat>()
                {
                  new GeneralChat()
                  {
                   CreationDate = DateTime.Now,
                   //Id = 1,
                   Title = "test",
                   BannedUsers = new List<User>(),
                   ChatBanned = false,
                   Owner=
                              new User() {
                    Email = "user@example.com",
                    //Id = 0,
                    Name = "string",
                    Salt = "SALT",
                         Invitations = new List<Invitation>()
                    {
                       new Invitation()
                       {
                        Accepted = false,
                        Seen = false,
                        DateSend = DateTime.Now,
                        //Id = 0,
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
                    //Id = 1,
                    StartDate = DateTime.Now,
                    Text = "test",
                    User =    new User() {
                    Email = "test",
                    //Id = 1,
                    Name = "test",
                    Salt = "SALT",
                         Invitations = new List<Invitation>()
                    {
                       new Invitation()
                       {
                        Accepted = false,
                        Seen = false,
                        DateSend = DateTime.Now,
                        //Id = 0,
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
                });
            _dbContext.GroupChatDatabase.AddRange(new List<GroupChat>()
            {
             new GroupChat()
             {
                CreationDate = DateTime.Now,
                Title = "test",
                GroupOwner =        new User() {
                Email = "test",
                //Id = 0,
                Name = "test",
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    //Id = 0,
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
                 //Id = 1,
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
                //Id = 1,
                Name = "test",
                Salt = "SALT",
                     Invitations = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    //Id = 0,
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
            });
            _dbContext.SaveChanges();
        }
    }
}
