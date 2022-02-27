using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MopApp.IdentityServer;
using MopApp.IdentityServer.MopAppIdentity;
using Services.Models;

namespace QuizApp.IdentityServer
{
    public class Startup
    {
        IHostEnvironment environment;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            environment = env;

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IUserStore<MopUser>, MopUserStore>();
            services.AddTransient<IRoleStore<MopUserRole>, MopUserRoleStore>();
            services.AddDbContext<moptaskDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddCors(o => o.AddPolicy("QuizAppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddIdentity<User, MopUserRole>()
                    .AddUserStore<MopUserStore>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddAspNetIdentity<User>()
                .AddProfileService<MopAppProfileService>();

            services.AddAuthentication();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
/*            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/
            app.UseDeveloperExceptionPage();
            app.UseCors("QuizAppPolicy");


            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();

        }
    }
}
