using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configs
{
    class TaskConfig : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.Description)
                .HasMaxLength(200);

            builder.Property(t => t.StoryPoints)
                .IsRequired();

            builder.Property(t => t.Status)
                .HasDefaultValue(Status.ToDo);

            builder.Property(t => t.Priority)
                .HasDefaultValue(Priority.Low);

            // Task has one User
            builder.HasOne(u => u.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
