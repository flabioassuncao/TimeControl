using Infra.Map;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using TimeControl.Models;

namespace Infra.Data.Context
{
   public class DataBaseContext : IdentityDbContext<ApplicationUser> 
   {
       public DbSet<Activity> Activities { get; set; }
       
       protected override void OnModelCreating(ModelBuilder builder)
        { 
            new ActivityMap(builder.Entity<Activity>());
             
            base.OnModelCreating(builder); 
        } 
   }
}