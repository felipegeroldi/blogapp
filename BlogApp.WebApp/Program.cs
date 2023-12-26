using BlogApp.DataAccess.Context;
using BlogApp.DataAccess.Repositories;
using BlogApp.DataAccess.Repositories.RepositoryInterfaces;
using BlogApp.WebApp.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace BlogApp.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BlogAppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default")));

            // Configure Cookie Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(4);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                });

            // Add email handler
            var emailSettings = builder.Configuration.GetSection("EmailSettings")
                .Get<EmailSettings>()!;

            builder.Services.AddSingleton(new EmailHandler(emailSettings));


            builder.Services.AddScoped<IUserRepository, UserRepository>();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
