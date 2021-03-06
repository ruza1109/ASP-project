﻿using DataAccess.Configs;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TeamworkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }
        public DbSet<UserUseCase> UserUseCases{ get; set; }
        public DbSet<UseCaseLogger> UseCaseLoggers{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BP50IAA\SQLEXPRESS;Initial Catalog=Teamworkv2;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new TaskConfig());
            modelBuilder.ApplyConfiguration(new TaskLogConfig());

            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId});

            modelBuilder.Entity<Role>().HasQueryFilter(r => r.DeletedAt == null);
            modelBuilder.Entity<User>().HasQueryFilter(r => r.DeletedAt == null);
            modelBuilder.Entity<Project>().HasQueryFilter(r => r.DeletedAt == null);
            modelBuilder.Entity<Task>().HasQueryFilter(r => r.DeletedAt == null);
            modelBuilder.Entity<TaskLog>().HasQueryFilter(r => r.DeletedAt == null);
        }

        public override int SaveChanges()
        {
            return HandleSaveChangesOverride();
        }

        private int HandleSaveChangesOverride()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity baseEntity)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            baseEntity.CreatedAt = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            /// <summary>
                            ///     If entity is soft deleted
                            ///     DeletedAt field is populated 
                            ///     UpdatedAt field keeps date if it already existed, if not - it's set to null
                            /// </summary>
                            /// <returns></returns>

                            if (baseEntity.DeletedAt == null)
                            {
                                baseEntity.UpdatedAt = DateTime.Now;
                            }
                            else
                            {
                                if (baseEntity.UpdatedAt == null)
                                {
                                    baseEntity.UpdatedAt = null;
                                }
                            }
                            break;

                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
