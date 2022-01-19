using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Cinerva.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string outFormat = "{yyy-MM-dd} HH:mmLss.fff} [{Level}] {Message}{NewLine}{Exception}";

            //Log.Logger = new LoggerConfiguration().WriteTo.File(@"Logs/log.txt", (Serilog.Events.LogEventLevel)RollingInterval.Day, outFormat).CreateLogger();

            CreateHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
