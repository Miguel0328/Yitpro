using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder
                .Property(x => x.EmployeeNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                 .Property(x => x.LastName)
                 .IsRequired()
                 .HasMaxLength(100);

            builder
                .Property(x => x.SecondLastName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(x => x.AdmissionDate)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.Password)
                .IsRequired(false)
                .HasMaxLength(300);

            builder
                .Property(x => x.PasswordLastUpdate)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.Manager)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Permissions)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermissionsModel>
    {
        public void Configure(EntityTypeBuilder<UserPermissionsModel> builder)
        {
            builder
                .HasKey(x => new { x.MenuId, x.UserId });

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
