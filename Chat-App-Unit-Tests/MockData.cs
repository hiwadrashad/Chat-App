using Chat_App_Library.Models;
using Chat_App_Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Unit_Tests
{
    public class MockData
    {
        public static User MOCKRETURN_USER = new User()
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
        };

        public static List<User> MOCKRETURN_USERS = new List<User>()
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

        public static Message MOCKRETURN_MESSAGE = new Message()
        {
            EndDate = DateTime.Now,
            Id = 1,
            StartDate = DateTime.Now,
            Text = "test",
            User = new User()
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
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"

            }
        };

        public static List<Message> MOCKRETURN_MESSAGES = new List<Message>()
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

        public static GroupChat MOCKRETURN_GROUPCHAT = new GroupChat()
        {
            CreationDate = DateTime.Now,
            Id = 1,
            Title = "test",
            GroupOwner = new User()
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
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "test"

            }
        };
        public static List<GroupChat> MOCKRETURN_GROUPCHATS = new List<GroupChat>()
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


        public static GeneralChat MOCKRETURN_GENERALCHAT = new GeneralChat()
        {
            CreationDate = DateTime.Now,
            Id = 1,
            Title = "test",
            BannedUsers = new List<User>(),
            ChatBanned = false,
            Owner =
                          new User()
                          {
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
                              HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password", "SALT")),
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

        };

        public static List<GeneralChat> MOCKRETURN_GENERALCHATS = new List<GeneralChat>()
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
        public static SingleUserChat MOCKRETURN_SINGLE_USER_CHAT = new SingleUserChat()
        {
            CreationDate = DateTime.Now,
            Id = 1,
            Title = "test",
            BannedUsers = new List<User>(),
            ChatBanned = false,
            MaxAmountPersons = 0,
            HashBase64 = "LynvGw5uWxVFi4bnuNqrWBByLwQFKoMF3XEEtIftGes=",
            Private = false,
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
        };

        public static List<SingleUserChat> MOCKRETURN_SINGLE_USER_CHATS = new List<SingleUserChat>()
            {
              new SingleUserChat()
              {
                CreationDate = DateTime.Now,
                Id = 1,
                Title = "test",
                BannedUsers = new List<User>(),
                ChatBanned = false,
                MaxAmountPersons = 0,
                HashBase64 = "LynvGw5uWxVFi4bnuNqrWBByLwQFKoMF3XEEtIftGes=",
                Private = false,
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

        public static List<Invitation> MOCKRETURN_INVITATIONS = new List<Invitation>()
                {
                   new Invitation()
                   {
                    Accepted = false,
                    Seen = false,
                    DateSend = DateTime.Now,
                    Id = 0,
                    Message = "Test"
                   }

        };

        public static Invitation MOCKRETURN_INVITATION = new Invitation()
        {
            Accepted = false,
            Seen = false,
            DateSend = DateTime.Now,
            Id = 0,
            Message = "Test"
        };

        public static AscendUserToAdminRequest MOCKRETURN_USERTOASCEND = new AscendUserToAdminRequest()
        {
            RequestingUser = new User()
            {
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
                HashBase64 = Convert.ToBase64String(Chat_App_Bussiness_Logic.Encryption.HashingAndSalting.GetHash("password", "SALT")),
                Role = Chat_App_Library.Enums.Role.Admin,
                Username = "string"

            },

           UserToAscend = 
            new User()
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
        };
    }
}
