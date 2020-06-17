using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using dashboard.Data;
using dashboard.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dashboard
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .AddRazorPagesOptions(options => 
                { 
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage"); 
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            services.AddAuthentication()
                .AddGoogle(options => 
                {
                    options.ClientId = "808936048574-tf5fa4oto6eljl4kvnflh4ica8dadmi4.apps.googleusercontent.com";
                    options.ClientSecret = "RHsjvdb4y9tWa927IHp6Rn7Z"; 
                    options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url"); 
                    options.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");
                    options.SaveTokens = true;
                    options.Events.OnCreatingTicket = ctx => 
                    { 
                        List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();
                        tokens.Add(new AuthenticationToken() 
                        {
                            Name = "TicketCreated", 
                            Value = DateTime.UtcNow.ToString()
                        });
                        ctx.Properties.StoreTokens(tokens);
                        return Task.CompletedTask;
                    };
                })
                .AddFacebook(options =>
                {
                    options.AppId = "281343409452957";
                    options.AppSecret = "d7323ade07f4169eeace8591b58a8aa6";
                })
                .AddTwitter(options =>
                {
                    options.ConsumerKey = "6jChk8EVXQUpwsNBImHAFx0ZA ";
                    options.ConsumerSecret = "eA8nunXKU2KgOFFmByL9tVKL4yujsujASn1CbW4VhfvGemlPX6 ";
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = "06f5257d-df03-4b9e-8c67-1d0f8d3b7d57";
                    options.ClientSecret = "Pzo/L@Ay.mId5ec]w0Q92Q_41UW46.?y";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
