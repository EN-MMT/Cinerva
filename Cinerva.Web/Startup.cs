using Cinerva.Services.Common.Properties;
using Cinerva.Services.Common.Properties.Dto;
using Cinerva.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cinerva.Services.Common.Cities.Dto;
using Cinerva.Services.Common.Cities;
using Cinerva.Services.Common.Users.Dto;
using Cinerva.Services.Common.Users;
using Microsoft.AspNetCore.Http;
using Serilog;
using Cinerva.Services;
using Cinerva.Web.Middleware;

namespace Cinerva.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .WriteTo.File(@"Logs/log.txt")
          .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CinervaDbContext>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IUserService, UserService>();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews();

            services.AddSingleton(Log.Logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMyCustomMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}