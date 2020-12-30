﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectModel>
    {
        public void Configure(EntityTypeBuilder<ProjectModel> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1200);

            builder
                .Property(x => x.UpdatedAt)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Leader)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ProjectTeamConfiguration : IEntityTypeConfiguration<ProjectTeamModel>
    {
        public void Configure(EntityTypeBuilder<ProjectTeamModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.UpdatedAt)
                .HasColumnType("smalldatetime");

            builder
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
