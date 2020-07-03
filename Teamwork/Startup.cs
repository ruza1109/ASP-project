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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Email;
using Implementation.Email;
using Implementation.Queries;
using Application.Commands.TaskLog;
using Implementation.Commands.TaskLogCommands;
using Implementation.Queries.TaskLogQueries;

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
            //services.AddDbContext<TeamworkContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("TeamworkDatabase"))); Didn't work after change arhictecture

            #endregion

            #region GlobalDependencies

            //  DbContext
            services.AddTransient<TeamworkContext>();

            //  AutoMapper Profiles
            //services.AddAutoMapper(this.GetType().Assembly); Didn't work after changing arhitecture
            services.AddAutoMapper(typeof(RoleProfile), typeof(UserProfile), typeof(ProjectProfile));

            //  Custom class for Fluent validator default error messages
            ValidatorOptions.LanguageManager = new CustomFluentErrorMessages();

            //  Commands
            services.AddTransient<CommandExecutor>();

            // Email
            services.AddTransient<IEmailSender, SmtpEmail>();

            //  Loggers
            services.AddTransient<IUseCaseLogger, DatabaseLogger>();
            //services.AddTransient<IUseCaseLogger, ConsoleLogger>();
            //services.AddTransient<IUseCaseLogger, FileLogger>();

            //  Application Actor
            //services.AddTransient<IApplicationActor, FakeAdminActor>();
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

            //  TaskLog CRUD Commands and Queries
            services.AddTransient<IGetTaskLogQuery, EFGetTaskLogQuery>();
            services.AddTransient<IGetOneTaskLogQuery, EFGetOneTaskLogQuery>();
            services.AddTransient<ICreateTaskLogCommand, EFCreateTaskLogCommand>();
            services.AddTransient<IUpdateTaskLogCommand, EFUpdateTaskLogCommand>();
            services.AddTransient<IDeleteTaskLogCommand, EFDeleteTaskLogCommand>();

            //  Logger 
            services.AddTransient<IGetLogsQuery, EFGetLogs>();

            #endregion

            #region Validations

            //  Role validations
            services.AddTransient<CreateRoleValidation>();
            services.AddTransient<UpdateRoleValidation>();

            //  User validations
            services.AddTransient<CreateUserValidation>();
            services.AddTransient<UpdateUserValidation>();

            //  Project Validations
            services.AddTransient<CreateProjectValidation>();
            services.AddTransient<UpdateProjectValidation>();

            //  Task Validations
            services.AddTransient<CreateTaskValidation>();
            services.AddTransient<UpdateTaskValidation>();

            //  TaskLog Validations
            services.AddTransient<CreateTaskLogValidation>();
            services.AddTransient<UpdateTaskLogValidation>();

            #endregion

            #region JWT

            services.AddHttpContextAccessor();

            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    throw new InvalidOperationException("Actor data doesn't exist.");
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JWTActor>(actorString);

                return actor;

            });

            services.AddTransient<JWTToken>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "teamwork_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion

        }

        //  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseRouting();

            app.UseMiddleware<AppExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
