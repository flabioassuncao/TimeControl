using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace TimeControl.Models
{
   public class DataBaseContext : IdentityDbContext<ApplicationUser> 
   {
       public DbSet<Activity> Activities { get; set; }
       
       public DbSet<Responsible> Responsibles { get; set; }
       
       protected override void OnModelCreating(ModelBuilder builder)
        { 
            builder.Entity<Activity>().HasKey(m => m.activityId);
            builder.Entity<Responsible>().HasKey(r => r.responsibleId);
             
            base.OnModelCreating(builder); 
        } 
   }
}