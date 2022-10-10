using EstateManagementApp.Data.Models;
using EstateManagementApp.Hubs;
using EstateManagementApp.Models;
using EstateManagementApp.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateManagementApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration _config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];

           // services.AddDbContextPool<AppDbContext>(
           services.AddDbContext<AppDbContext>(
              options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection"))
          );
            
            services.AddScoped<IAddCategoryRepository, AddCategoryRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
               

                options.SignIn.RequireConfirmedEmail = false;
          //      options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

            //    options.Lockout.MaxFailedAccessAttempts = 5;
              //  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<AppDbContext>();
            //services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            //services.AddTransient<IMailService, Services.MailService>();
            services.AddControllersWithViews();
            services.Configure<StripeSetting>(_config.GetSection("Stripe"));
            services.AddRazorPages();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(_config.GetSection("Stripe")["SecretKey"]);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
               
                endpoints.MapHub<ChatHub>("/chathub");

            });
        }
    }
}
