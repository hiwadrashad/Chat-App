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
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chat-App;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
