using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;


namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                        .WriteTo.Email(new EmailConnectionInfo
                        {
                            FromEmail = "aliakseikartashou@gmail.com",
                            ToEmail = "homavex941@scubalm.com",
                            MailServer = "smtp.gmail.com",
                            NetworkCredentials = new NetworkCredential("aliakseikartashou@gmail.com", ""),
                            EnableSsl = true,
                            Port = 465,
                            EmailSubject = "Serilog Log Event"
                        }, restrictedToMinimumLevel: LogEventLevel.Error, batchPostingLimit: 1);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
