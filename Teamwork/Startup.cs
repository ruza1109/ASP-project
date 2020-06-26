using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Implementation.Validations;
using Application.Commands;
using Teamwork.App;
using Application;
using Application.Commands.Role;
using Implementation.Commands.RoleCommands;
using Implementation.Queries.RoleQueries;
using Application.Commands.User;
using Implementation.Queries.UserQueries;
using Implementation.Commands.UserCommands;
using Application.Commands.Project;
using Implementation.Queries.ProjectQueries;
using Implementation.Commands.ProjectCommands;
using Application.Commands.Task;
using Implementation.Queries.TaskQuery;
using Implementation.Commands.TaskCommands;
using Implementation.Loggers;
using Implementation.Profiles;
using Domain.Entities;

namespace Teamwork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region DatabaseConfig

            //  ConnectionString
            services.AddDbContext<TeamworkContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TeamworkDatabase")));

            #endregion

            #region GlobalDependencies

            //  DbContext
            services.AddTransient<TeamworkContext>();

            //  AutoMapper
            //services.AddAutoMapper(this.GetType().Assembly); Didn't work after changing arhitecture
            services.AddAutoMapper(typeof(RoleProfile));

            //  Custom class for Fluent validator default error messages
            ValidatorOptions.LanguageManager = new CustomFluentErrorMessages();

            //  Commands
            services.AddTransient<CommandExecutor>();

            //  Logger
            services.AddTransient<IUseCaseLogger, FileLogger>(); // ConsoleLogger is also an option

            //  Application Actor
            services.AddTransient<IApplicationActor, FakeActor>();
            #endregion

            #region EntityDependencies

            //  Role CRUD Commands and Queries
            services.AddTransient<IGetRoleQuery, EFGetRoleQuery>();
            services.AddTransient<IGetOneRoleQuery, EFGetOneRoleQuery>();
            services.AddTransient<ICreateRoleCommand, EFCreateRoleCommand>();
            services.AddTransient<IUpdateRoleCommand, EFUpdateRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EFDeleteRoleCommand>();

            //  User CRUD Commands and Queries
            services.AddTransient<IGetUserQuery, EFGetUserQuery>();
            services.AddTransient<IGetOneUserQuery, EFGetOneUserQuery>();
            services.AddTransient<ICreateUserCommand, EFCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EFUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EFDeleteUserCommand>();

            //  Project CRUD Commands and Queries
            services.AddTransient<IGetProjectQuery, EFGetProjectQuery>();
            services.AddTransient<IGetOneProjectQuery, EFGetOneProjectQuery>();
            services.AddTransient<ICreateProjectCommand, EFCreateProjectCommand>();
            services.AddTransient<IUpdateProjectCommand, EFUpdateProjectCommand>();
            services.AddTransient<IDeleteProjectCommand, EFDeleteProjectCommand>();

            //  Task CRUD Commands and Queries
            services.AddTransient<IGetTaskQuery, EFGetTaskQuery>();
            services.AddTransient<IGetOneTaskQuery, EFGetOneTaskQuery>();
            services.AddTransient<ICreateTaskCommand, EFCreateTaskCommand>();
            services.AddTransient<IUpdateTaskCommand, EFUpdateTaskCommand>();
            services.AddTransient<IDeleteTaskCommand, EFDeleteTaskCommand>();

            #endregion

            #region Validations

            //  Role validations
            services.AddTransient<CreateRoleValidation>();
            services.AddTransient<UpdateRoleValidation>();
            services.AddTransient<CreateUserValidation>();
            services.AddTransient<UpdateUserValidation>();

            #endregion

        }

        //  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<AppExceptionHandler>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
