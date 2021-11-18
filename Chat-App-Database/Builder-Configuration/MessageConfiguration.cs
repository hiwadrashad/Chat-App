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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        private const string usertablename = "Message";
        private const int contentmaxlength = 255;
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable(usertablename);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Text).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.User).IsRequired();
            builder.Property(a => a.StartDate).IsRequired();
        }
    }
}
