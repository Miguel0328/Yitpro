using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class ViewConfigurations
    {
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
