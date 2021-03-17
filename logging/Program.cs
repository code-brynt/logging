using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;

namespace logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // use this switch to change the logging level at runtime
            var levelSwitch = new LoggingLevelSwitch
            {
                MinimumLevel = LogEventLevel.Verbose
            };
            var template = "{Timestamp:yyyy-MM-ddTHH:mm:sszzz} | SECURITY_EVENT | {Message}{NewLine}";

            // the SecurityEventFilter will enable only the required logevents to the Debug console
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.ControlledBy(levelSwitch)
                 .WriteTo.Console()
                 .WriteTo.Debug(outputTemplate: template)
                 .Filter.With<SecurityEventFilter>()
                 .CreateLogger();

            try 
            {
                Log.Information("Starting Web Host");
                // pass the switch into the host builder so it can be registered in the IoC
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
                // setup serilog
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                // register the switch in the IoC
                .ConfigureServices((_, services) =>
                    services.AddSingleton(levelSwitch));
    }
}
