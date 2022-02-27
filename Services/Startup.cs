using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Services.Services;
using Services.Models;
using Services.Repositories;
using Microsoft.AspNetCore.Http;

namespace Services
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
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IQuestionsAnswersService, QuestionsAnswersService>();
            services.AddScoped<IQuestionsAnswersRepository, QuestionsAnswersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddDbContext<moptaskDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection"));
            });


            services.AddCors(
              c => c.AddPolicy("MopTaskPolicy", options =>
              {
                  options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyHeader();
              }));

            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
            });

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = Configuration["MOPTaskAuthority"];
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "MOPTask.Services";
                        options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Jwt;
                    });

            services.AddDbContext<moptaskDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MopTaskPolicy");

            app.UseAuthentication();
          
            app.UseMvc();

            app.UseHttpsRedirection();

            //app.UseRouting();

        //    app.UseAuthorization();

            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            */
        }
    }
}
