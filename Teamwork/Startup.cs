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
using Teamwork.App.Validations;
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

            //  Database config
            services.AddDbContext<TeamworkContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TeamworkDatabase")));

            //  DI
            services.AddTransient<TeamworkContext>();
            services.AddAutoMapper(this.GetType().Assembly);
            
            //  Custom class for Fluent validator default error messages
            ValidatorOptions.LanguageManager = new CustomFluentErrorMessages();

            //  Validations
            services.AddTransient<CreateRoleValidation>();
            services.AddTransient<CreateUserValidation>();

        }

        //  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
