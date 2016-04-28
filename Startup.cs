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
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            
                
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            var connection = @"Server=localhost;port=5432;Database=ActivityDB;Username=postgres;Password=postgres";
 
            services.AddEntityFramework()
                .AddNpgsql()
                .AddDbContext<DataBaseContext>(options => options.UseNpgsql(connection));
                
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders();
            
            // Add framework services.
            services.AddMvc().AddJsonOptions(options => {
                // options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            services.AddMvc();
            
            //using Dependency Injection
            services.AddSingleton<IActivityRepository, ActivityRepository>();
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();
 
            app.UseIdentity();
            
            app.UseStaticFiles();
            
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
