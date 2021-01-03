using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class CatalogConfiguration : IEntityTypeConfiguration<CatalogModel>
    {
        public void Configure(EntityTypeBuilder<CatalogModel> builder)
        {
            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(x => x.Alias)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
