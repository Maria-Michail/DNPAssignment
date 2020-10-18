using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Assignment.Data;
using Models;
using Assignment.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using FileData;

namespace Assignment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<IUserService, InMemoryUserService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IGetFamilies, GetFamilies>();
            services.AddScoped<FileContext>();

            services.AddAuthorization(options => {
            options.AddPolicy("MustBeVIA",  a => 
                a.RequireAuthenticatedUser().RequireClaim("Domain", "via.dk"));
            
            options.AddPolicy("SecurityLevel4",  a => 
                a.RequireAuthenticatedUser().RequireClaim("Level", "4","5"));
            
            options.AddPolicy("MustBeTeacher",  a => 
                a.RequireAuthenticatedUser().RequireClaim("Role", "Teacher"));
            
            options.AddPolicy("SecurityLevel2", policy =>
                policy.RequireAuthenticatedUser().RequireAssertion(context => {
                    Claim levelClaim = context.User.FindFirst(claim => claim.Type.Equals("Level"));
                    if (levelClaim == null) return false;
                    return int.Parse(levelClaim.Value) >= 2;
                }));
        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
