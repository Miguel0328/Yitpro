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
                .Property(x => x.Controller)
                .HasMaxLength(50);

            builder
                .Property(x => x.Action)
                .HasMaxLength(100);

            builder
                .HasOne(x => x.Parent)
                .WithOne()
                .HasForeignKey<MenuModel>(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
