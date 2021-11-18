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

    public class SingleUserChatConfiguration : IEntityTypeConfiguration<SingleUserChat>
    {
        private const string usertablename = "SingleUserChat";
        private const int contentmaxlength = 255;

        public void Configure(EntityTypeBuilder<SingleUserChat> builder)
        {
            builder.ToTable(usertablename);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Messages).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.Title).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.OriginUser).IsRequired();
            builder.Property(a => a.RecipientUser).IsRequired();
            builder.Property(a => a.CreationDate).IsRequired();
            builder.Property(a => a.Messages).IsRequired();
        }
    }
}
