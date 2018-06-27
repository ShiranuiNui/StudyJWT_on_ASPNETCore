using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudyJWT_on_ASPNETCore.Database;
using StudyJWT_on_ASPNETCore.Model;

namespace StudyJWT_on_ASPNETCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /* fixformat ignore:start */
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Issuer"],

                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Configuration["Jwt:Key"])),

                        ValidateLifetime = true,
                        
                    };
                });
            services.AddMvc().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
        }
        /* fixformat ignore:end */

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts ();
            }

            // app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            // Seed Database
            Database.Database.UserDatabase.Add(new Model.User
            {
                Username = "KizunaAi",
                    Password = "password",
                    Name = "Kizuna Ai",
                    Email = "ai@example.com",
                    Role = "ADMIN"
            });
        }
    }
}