using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolidol.Model.Model;

namespace Toolidol.Model.EntityConfigurations
{
    public class FacebookPageConfiguration : IEntityTypeConfiguration<FacebookPage>
    {
        public void Configure(EntityTypeBuilder<FacebookPage> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("FacebookPages");

            builder.Property(p => p.FacebookPageId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.EncryptedAccessToken)
                .IsRequired();

            builder.HasOne(p => p.User) 
                   .WithMany(u => u.FacebookPages)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => new { p.UserId, p.FacebookPageId })
                   .IsUnique()
                   .HasDatabaseName("UQ_FacebookPages_UserId_FacebookPageId");
        }
    }
}
