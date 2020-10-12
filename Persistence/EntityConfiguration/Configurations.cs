using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class Configurations
    {
        public class UserConfiguration : IEntityTypeConfiguration<UserModel>
        {
            public void Configure(EntityTypeBuilder<UserModel> builder)
            {
                builder
                    .Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                builder
                     .Property(X => X.LastName)
                     .IsRequired()
                     .HasMaxLength(50);

                builder
                    .Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                builder
                    .Property(x => x.EmployeeNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                builder
                    .Property(x => x.Password)
                    .HasMaxLength(500);
            }
        }

        public class ViewConfiguration : IEntityTypeConfiguration<ViewModel>
        {
            public void Configure(EntityTypeBuilder<ViewModel> builder)
            {
                builder.HasKey(x => x.Id);

                builder
                    .Property(x => x.Controller)
                    .IsRequired()
                    .HasMaxLength(50);

                builder
                    .Property(X => X.View)
                    .IsRequired()
                    .HasMaxLength(50);
            }
        }
    }
}
