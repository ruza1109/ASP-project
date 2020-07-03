using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configs
{
    public class TaskLogConfig : IEntityTypeConfiguration<TaskLog>
    {
        public void Configure(EntityTypeBuilder<TaskLog> builder)
        {
            builder.Property(t => t.Time)
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(200);

            builder.Property(t => t.Date)
                .HasDefaultValue()
                .IsRequired();

            builder.HasOne(t => t.Task)
                .WithOne(tl => tl.Log)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
