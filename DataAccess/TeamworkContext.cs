﻿using DataAccess.Configs;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TeamworkContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BP50IAA\SQLEXPRESS;Initial Catalog=Teamwork;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new TaskConfig());

            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId});
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }



    }
}
