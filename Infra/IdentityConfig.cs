using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using TimeControl.Models;

namespace TimeControl.Infra
{
	public static class IdentityConfig
	{
		public static void BootstrapIdentity(IServiceCollection services)
        {
			AddIdentityUser(services);
			ConfigureIdentity(services);
        }
		private static void AddIdentityUser(IServiceCollection services)
        {
        	services.AddIdentity<Person, IdentityRole>()
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders();
        }
		
		private static void ConfigureIdentity(IServiceCollection services)
        {
			services.Configure<IdentityOptions>(config =>
			{
				config.Cookies.ApplicationCookie.Events = PreventRedirect(); 
			});    
		}
		
		private static ICookieAuthenticationEvents PreventRedirect()
			=> new CookieAuthenticationEvents()	{ OnRedirectToLogin = ctx => OnRedirectToLogin(ctx)	};
		
		private static Task OnRedirectToLogin(CookieRedirectContext context)
		{
			if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
				return Task.FromResult<object>(null);

			context.Response.Redirect(context.RedirectUri);
			return Task.FromResult<object>(null);
		}
	}        
}