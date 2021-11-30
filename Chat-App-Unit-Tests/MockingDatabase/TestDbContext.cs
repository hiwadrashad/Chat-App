using Chat_App_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat_App_Unit_Tests.MockingDatabase
{
    public class TestDbContext : DbContext
    {
        public DbSet<Chat_App_Library.Models.User> UserDatabase { get; set; }
        public DbSet<Chat_App_Library.Models.GroupChat> GroupChatDatabase { get; set; }
        public DbSet<Chat_App_Library.Models.GeneralChat> GeneralChatDatabase { get; set; }
        public DbSet<Chat_App_Library.Models.SingleUserChat> SingleUserChatDatabase { get; set; }
        public DbSet<Chat_App_Library.Models.Message> MessageDatabase { get; set; }
        public DbSet<Chat_App_Library.Models.RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chat-App-Xunit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new Chat_App_Database.Builder_Configuration.MessageConfiguration());
            //modelBuilder.ApplyConfiguration(new Chat_App_Database.Builder_Configuration.GeneralChatConfiguration());
            //modelBuilder.ApplyConfiguration(new Chat_App_Database.Builder_Configuration.GroupChatConfiguration());
            //modelBuilder.ApplyConfiguration(new Chat_App_Database.Builder_Configuration.RefreshConfiguration());
            //modelBuilder.ApplyConfiguration(new Chat_App_Database.Builder_Configuration.UserConfiguration());
        }
    }
}
