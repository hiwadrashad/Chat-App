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
    public class GeneralChatConfiguration : IEntityTypeConfiguration<GeneralChat>
    {
        private const string usertablename = "GeneralChat";
        private const int contentmaxlength = 255;
        public void Configure(EntityTypeBuilder<GeneralChat> builder)
        {
            builder.ToTable(usertablename);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Messages).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.Title).HasMaxLength(contentmaxlength).IsRequired();
            builder.Property(a => a.CreationDate).IsRequired();
        }
    }
}
