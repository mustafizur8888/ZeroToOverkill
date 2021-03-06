using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using LogLevel = NLog.LogLevel;

namespace ZeroToOverkill
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigureNLog();
            var host = CreateHostBuilder(args).Build();
            await host.EnsureDbUpToDate();
            host.Run();
        }

        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("coloredConsole")
            {
                Layout = @"${data:format=HH\:mm\:ss} ${logger} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("file")
            {
                FileName = "${basedir}/file.log",
                Layout = @"${data:format=HH\:mm\:ss} ${level} ${message} ${exception} ${ndlc}"
            };
            config.AddTarget(fileTarget);
            // config.AddRule(LogLevel.Trace, LogLevel.Info, consoleTarget, "ZeroToOverkill.*");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
