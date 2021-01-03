using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(300);

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Projects)
                .WithOne(x => x.Client)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
