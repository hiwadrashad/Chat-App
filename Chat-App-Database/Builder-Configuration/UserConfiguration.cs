using Chat_App_Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Database.Builder_Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private const string usertablename = "User";
        private const int contentmaxlength = 255;
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(usertablename);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AttemptedPassword).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.Email).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.Username).HasMaxLength(contentmaxlength).IsRequired();
        }
    }
}
