using Huawei.RabbitMqSubscriberService.Models;
using Huawei.RabbitMqSubscriberService.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Serilog;

namespace Huawei.RabbitMqSubscriberService
{
    public class Program
    {

        public static int Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();

                Log.Information("Stopped cleanly");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSystemd()
            .ConfigureAppConfiguration(
                (hostContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", false, true);
                    config.AddCommandLine(args);
                })
            .ConfigureLogging(loggingBuilder =>
            {
                var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();
                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
                loggingBuilder.AddSerilog(logger, dispose: true);
            })
            .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration Configuration = hostContext.Configuration;
                    services.AddDbContext<MailValidationContext>(options =>
                    {
                        options.UseNpgsql(Configuration.GetConnectionString("Postgres")).EnableSensitiveDataLogging();
                    });
                    services.AddSingleton<RabbitMQClientService>();
                    services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
                    services.AddHostedService<Worker>();
                });





    }
}