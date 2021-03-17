using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var levelSwitch = new LoggingLevelSwitch();
            levelSwitch.MinimumLevel = LogEventLevel.Information;

            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.ControlledBy(levelSwitch)
                 .WriteTo.Console().Filter.With<SecurityEventFilter>()
                 .CreateLogger();

            try
            {
                Log.Information("Starting Web Host");
                CreateHostBuilder(args, levelSwitch).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, LoggingLevelSwitch levelSwitch) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((_, services) =>
                    services.AddSingleton(levelSwitch));
    }
}
