using Chat_App_Database.Builder_Configuration;
using Chat_App_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Database.Database
{
    public class ChatDbContext : DbContext
    {
        public DbSet<User> UserDatabase { get; set; }
        public DbSet<GroupChat> GroupChatDatabase { get; set; }
        public DbSet<GeneralChat> GeneralChatDatabase { get; set; }
        public DbSet<SingleUserChat> SingleUserChatDatabase { get; set; }
        public DbSet<Message> MessageDatabase { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chat-App-Backend;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MessageConfiguration);
            modelBuilder.Entity<Message>().HasData(new Message()
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
                    HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
                    Role = Chat_App_Library.Enums.Role.Admin,
                    Username = "test"

                }
            };
            )
            //modelBuilder.Entity<SingleUserChat>().HasData(
            //      new SingleUserChat()
            //      {
            //          OriginUser = new User()
            //          {
            //              Email = "test",
            //              Id = 1,
            //              Name = "test",
            //              Salt = "SALT",
            //              Invitations = new List<Invitation>()
            //    {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //    },
            //              Banned = false,
            //              HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            //              Username = "test"
            //          },
            //          RecipientUser = new User()
            //          {
            //              Email = "test",
            //              Id = 2,
            //              Name = "test",
            //              Salt = "SALT",
            //              Invitations = new List<Invitation>()
            //    {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //    },
            //              Banned = false,
            //              HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            //              Username = "test"
            //          },
            //          CreationDate = DateTime.Now,
            //          Id = 1,
            //          Title = "test",
            //          BannedUsers = new List<User>(),
            //          ChatBanned = false,
            //          MaxAmountPersons = 0,
            //          HashBase64 = "LynvGw5uWxVFi4bnuNqrWBByLwQFKoMF3XEEtIftGes=",
            //          Private = false,
            //          Messages = new List<Message>()
            //    {
            //         new Message()
            //         {
            //          EndDate = DateTime.Now,
            //          Id = 1,
            //          StartDate = DateTime.Now,
            //          Text = "test",
            //          User =    new User() {
            //          Email = "test",
            //          Id = 1,
            //          Name = "test",
            //          Salt = "SALT",
            //               Invitations = new List<Invitation>()
            //    {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //    },
            //          Banned = false,
            //    HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password","SALT")),
            //          Role = Chat_App_Library.Enums.Role.Admin,
            //          Username = "test"

            //         }
            //    }

            //    }
            //      }
            //    );
            //modelBuilder.Entity<GeneralChat>().HasData(
            //   new GeneralChat()
            //   {
            //       CreationDate = DateTime.Now,
            //       Id = 1,
            //       Title = "test",
            //       BannedUsers = new List<User>(),
            //       ChatBanned = false,
            //       Owner =
            //              new User()
            //              {
            //                  Email = "user@example.com",
            //                  Id = 0,
            //                  Name = "string",
            //                  Salt = "SALT",
            //                  Invitations = new List<Invitation>()
            //    {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //    },
            //                  Banned = false,
            //                  HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            //                  Role = Chat_App_Library.Enums.Role.Admin,
            //                  Username = "string"

            //              },
            //       MaxAmountPersons = 0,
            //       Password = "password",
            //       Messages = new List<Message>()
            //   {
            //           new Message()
            //   {
            //    EndDate = DateTime.Now,
            //    Id = 1,
            //    StartDate = DateTime.Now,
            //    Text = "test",
            //    User =    new User() {
            //    Email = "test",
            //    Id = 1,
            //    Name = "test",
            //    Salt = "SALT",
            //         Invitations = new List<Invitation>()
            //    {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //    },
            //    Banned = false,
            //    HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password","SALT")),
            //    Role = Chat_App_Library.Enums.Role.Admin,
            //    Username = "test"

            //  }
            //   }
            //   }

            //   });
            //modelBuilder.Entity<GroupChat>().HasData(
            //    new GroupChat()
            //    {
            //        CreationDate = DateTime.Now,
            //        Id = 1,
            //        Title = "test",
            //        GroupOwner = new User()
            //        {
            //            Email = "test",
            //            Id = 1,
            //            Name = "test",
            //            Salt = "SALT",
            //            Invitations = new List<Invitation>()
            //    {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //    },
            //            Banned = false,
            //            HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            //            Role = Chat_App_Library.Enums.Role.Admin,
            //            Username = "test"

            //        },
            //        Messages = new List<Message>()
            // {
            //    new Message()
            // {
            //     EndDate = DateTime.Now,
            //     Id = 1,
            //     StartDate = DateTime.Now,
            //     Text = "test",
            // }
            // },
            //        BannedUsers = new List<User>(),
            //        ChatBanned = false,
            //        MaxAmountPersons = 0,
            //        HashBase64 = "LynvGw5uWxVFi4bnuNqrWBByLwQFKoMF3XEEtIftGes=",
            //        Private = false,
            //        Users = new List<User>()
            // {
            //   new User() {
            //    Email = "test",
            //    Id = 1,
            //    Name = "test",
            //    Salt = "SALT",
            //         Invitations = new List<Invitation>()
            //    {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //    },
            //    Banned = false,
            //    HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password","SALT")),
            //    Role = Chat_App_Library.Enums.Role.Admin,
            //    Username = "test"

            //  }
            // }

            //    });
            //modelBuilder.Entity<User>().HasData(
            //     new User()
            //     {
            //         Email = "user@example.com",
            //         Id = 0,
            //         Name = "string",
            //         Salt = "SALT",
            //         Invitations = new List<Invitation>()
            //     {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //     },
            //         Banned = false,
            //         HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            //         Role = Chat_App_Library.Enums.Role.Admin,
            //         Username = "string"

            //     },
            //   new User()
            //   {
            //       Email = "test",
            //       Id = 1,
            //       Name = "test",
            //       Salt = "SALT",
            //       Invitations = new List<Invitation>()
            //     {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //     },
            //       Banned = false,
            //       HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            //       Username = "test"

            //   },
            //   new User()
            //   {
            //       Email = "hiwad.rashad@itvitaelearning.nl",
            //       Id = 2,
            //       Name = "test",
            //       Salt = "SALT",
            //       Invitations = new List<Invitation>()
            //     {
            //       new Invitation()
            //       {
            //        Accepted = false,
            //        Seen = false,
            //        DateSend = DateTime.Now,
            //        Id = 0,
            //        Message = "Test"
            //       }
            //     },
            //       Banned = false,
            //       HashBase64 = Convert.ToBase64String(Chat_App_Database.Encryption.HashingAndSalting.GetHash("password", "SALT")),
            //       Username = "test"

            //   });
        }
    }
}
