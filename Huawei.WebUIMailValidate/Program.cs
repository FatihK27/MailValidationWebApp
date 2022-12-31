using Huawei.WebUIMailValidate.Models;
using Huawei.WebUIMailValidate.SharedModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huawei.WebUIMailValidate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            using (var scope = host.Services.CreateScope())
            {
                //UseUrls("http://localhost:5003", "https://localhost:5004";
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                appDbContext.Database.Migrate();
                if (!appDbContext.Users.Any())
                {
                    userManager.CreateAsync(new User() { UserName = "deneme", Email = "deneme@outlook.com" }, "Password12*").Wait();
                    userManager.CreateAsync(new User() { UserName = "deneme2", Email = "deneme2@outlook.com" }, "Password12*").Wait();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
