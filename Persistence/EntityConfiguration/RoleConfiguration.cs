using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleModel>
    {
        public void Configure(EntityTypeBuilder<RoleModel> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(100);

            builder
                .Property(x => x.CreatedAt)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
