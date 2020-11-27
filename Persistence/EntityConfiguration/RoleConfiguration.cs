﻿using Microsoft.EntityFrameworkCore;
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
                .Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class RolePermissionsConfiguration : IEntityTypeConfiguration<RolePermissionsModel>
    {
        public void Configure(EntityTypeBuilder<RolePermissionsModel> builder)
        {
            builder
                .HasKey(x => new { x.MenuId, x.RoleId });

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
