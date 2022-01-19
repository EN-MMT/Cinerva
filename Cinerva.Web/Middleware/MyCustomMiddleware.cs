using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Events;
using Serilog;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Management.Monitor.Fluent.Models;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Cinerva.Web.Middleware
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger log=null;
        private readonly IConfiguration cfg;

        private void GrantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }

        //private readonly LogSettings logSettings;
        //static readonly ILogger Log = Serilog.Log.ForContext<MyCustomMiddleware>();
        public MyCustomMiddleware(RequestDelegate next, IConfiguration cfg)
        {
            _next = next;
            this.cfg = cfg;

        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext)
        {

            
            Log.Logger = (ILogger)new LoggerConfiguration()
                .WriteTo
                .Console()
                .WriteTo
                .File(cfg.GetSection("Logs").GetSection("Path").Value,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                rollingInterval: RollingInterval.Day
                //cfg.GetSection("Logs").GetSection("Format").Value
                )
                .CreateLogger();

            GrantAccess(cfg.GetSection("Logs").GetSection("Path").Value);

            //Console.WriteLine("LOL");
            await _next(httpContext);
            //Console.WriteLine("LOL2");
            Console.WriteLine(cfg.GetSection("Logs").GetSection("Path").Value);
            Console.WriteLine(httpContext.Response.StatusCode);
            //svc.Write(DateTime.Now.Ticks.ToString());
            //svc.Write(httpContext.Response.StatusCode.ToString());
            var response = $"HTTP {httpContext.Response.StatusCode.ToString()} {httpContext.Request.Path} responded {httpContext.Response.StatusCode}";
            Log.Write(LogEventLevel.Information,response);
            Console.WriteLine(response);
            
        }
    }

    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
