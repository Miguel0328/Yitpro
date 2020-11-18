using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<MenuModel>
    {
        public void Configure(EntityTypeBuilder<MenuModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.Description)
                .HasMaxLength(100);

            builder
                .Property(x => x.Route)
                .HasMaxLength(100)
                .IsRequired(false);

            builder
                .HasOne(x => x.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
