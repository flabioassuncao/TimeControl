using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TimeControl.Models;
using TimeControl.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using Infra.Data.Context;
using TimeControl.Interfaces.Repository;
using TimeControl.Application.Interface;
using TimeControl.Service.Application;
using Newtonsoft.Json;

namespace TimeControl
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=localhost;port=5432;Database=ActivityDB;Username=postgres;Password=postgres";
 
            services.AddEntityFramework()
                .AddNpgsql()
                .AddDbContext<DataBaseContext>(options => options.UseNpgsql(connection));
                
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders();
            
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc();

            services.AddSingleton<IActivityRepository, ActivityRepository>();
            services.AddSingleton<ITimeRepository, TimeRepository>();
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<ITimeService, TimeService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseIISPlatformHandler();
            app.UseIdentity();
            app.UseStaticFiles();
            app.UseMvc();
        }

        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
