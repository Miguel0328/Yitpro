using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class ActivityConfiguration : IEntityTypeConfiguration<ActivityModel>
    {
        public void Configure(EntityTypeBuilder<ActivityModel> builder)
        {
            builder.
                Property(x => x.Description)
                .HasMaxLength(3000);

            builder
                .Property(x => x.Requirement)
                .HasMaxLength(500);

            builder
                .Property(x=>x.StartDate)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.EndDate)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.FinalDate)
                .HasColumnType("smalldatetime");

            builder
                .Property(x => x.UpdatedAt)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.Phase)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Clasification)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Assigned)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Responsible)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Comments)
                .WithOne(x => x.Activity)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ActivityCommentConfiguration : IEntityTypeConfiguration<ActivityCommentModel>
    {
        public void Configure(EntityTypeBuilder<ActivityCommentModel> builder)
        {
            builder.
                Property(x => x.Comment)
                .HasMaxLength(3000);

            builder
                .Property(x => x.Date)
                .HasColumnType("smalldatetime");
        }
    }
}
