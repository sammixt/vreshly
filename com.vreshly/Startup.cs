using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Data;
using BLL.Infrastructure.Data;
using BLL.Infrastructure.Identity;
using BLL.Infrastructure.Services;
using BLL.Interface;
using com.vreshly.EmailProcessor;
using com.vreshly.Extensions;
using com.vreshly.Helper;
using com.vreshly.Middleware;
using com.vreshly.Models;
using com.vreshly.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using StackExchange.Redis;

namespace com.vreshly
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
            services.AddControllersWithViews();
            services.AddDbContext<StoreContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMailer, Mailer>();
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IReadTemplate, ReadTemplate>();
            services.AddScoped<IDashboardService,DashboardService>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IRecurringOrderService, RecurringOrderService>();
            services.AddAutoMapper(typeof(MappingProfiles));
            //services.Configure<ApiBehaviourOptions>
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Home");
                    o.SlidingExpiration = true;
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    //o.Cookie.Expiration = TimeSpan.FromMinutes(60);
                });
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator",
                     policy => policy.RequireRole("Administrator"));
                options.AddPolicy("StoreKeeper",
                     policy => policy.RequireRole("StoreKeeper"));
                //options.AddPolicy(Policy.NewUser,
                //    policy => policy.RequireAssertion(context =>
                //       context.User.HasClaim(c =>
                //       (c.Type == "UserStatus" && (c.Value.ToString() == "Unconfirmed" || c.Value.ToString() == "Confirmed"))
                //       ))
                //    );
            });
            //services.AddSingleton<IConnectionMultiplexer>(c =>
            //{
            //    var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("redis"), true);
            //    return ConnectionMultiplexer.Connect(configuration);
            //});
            services.AddIdentityServices(Configuration);
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
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
            });
        }
    }
}
