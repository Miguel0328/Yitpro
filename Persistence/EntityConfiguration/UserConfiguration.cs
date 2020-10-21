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
                .HasMaxLength(50);

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(100);

            builder
                 .Property(X => X.LastName)
                 .HasMaxLength(100);

            builder
                .Property(x => x.Email)
                .HasMaxLength(200);

            builder
                .Property(x => x.AdmissionDate)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.Password)
                .HasMaxLength(500);

            builder
                .Property(x => x.PasswordLastUpdate)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.CreatedAt)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.Manager)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

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
