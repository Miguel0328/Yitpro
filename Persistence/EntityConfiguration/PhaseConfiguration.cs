using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class PhaseConfiguration : IEntityTypeConfiguration<PhaseModel>
    {
        public void Configure(EntityTypeBuilder<PhaseModel> builder)
        {
            builder
                .HasKey(x => new { x.PhaseId, x.ClasificationId });

            builder
                .HasOne(x => x.Phase)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Clasification)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

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
