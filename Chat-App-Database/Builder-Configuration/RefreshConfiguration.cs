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
    public class RefreshConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        private const string usertablename = "RefreshToken";
        private const int contentmaxlength = 255;

        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(usertablename);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AddedDate).IsRequired();
            builder.Property(a => a.ExpiryDate).IsRequired();
            builder.Property(a => a.IsRevoked).IsRequired();
            builder.Property(a => a.IsUsed).IsRequired();
            builder.HasOne(a => a.jwtId).WithMany().IsRequired();
            builder.Property(a => a.Token).IsRequired();
            builder.Property(a => a.User).IsRequired();
            builder.Property(a => a.UserId).IsRequired();
        }
    }
}
