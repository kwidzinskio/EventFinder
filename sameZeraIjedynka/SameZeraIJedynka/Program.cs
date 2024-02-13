using SameZeraIjedynka.Database.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SameZeraIjedynka.Database.Repositories;
using SameZeraIjedynka.BusinnessLogic.Services;
using SameZeraIJedynka.BusinnessLogic.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SameZeraIJedynka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Serilog logger
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Users/Login"; 
            options.LogoutPath = "/Users/Logout"; 
            options.AccessDeniedPath = "/Users/AccessDenied"; 
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
        });
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddDbContext<DatabaseContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContextConnectionString")));

            // Services Dependency Injection
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserFavoriteService, UserFavoriteService>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserFavoriteRepository, UserFavoriteRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}