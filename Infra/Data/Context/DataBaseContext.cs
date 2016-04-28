using Infra.Map;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using TimeControl.Models;

namespace Infra.Data.Context
{
   public class DataBaseContext : IdentityDbContext<ApplicationUser> 
   {
       public DbSet<Activity> Activities { get; set; }
       public DbSet<Time> Times { get; set; }
       public DbSet<Project> Projects { get; set; }
       public DbSet<User> User { get; set; }
    //    public DbSet<UsersProjects> UsersProjects { get; set;}
       public DbSet<BelongToProject> BelongToProjects { get; set; }   
       protected override void OnModelCreating(ModelBuilder builder)
        { 
            new ActivityMap(builder.Entity<Activity>());
            new TimeMap(builder.Entity<Time>());
            new ProjectMap(builder.Entity<Project>());
            new UserMap(builder.Entity<User>());
            new UsersProjectsMap(builder.Entity<UsersProjects>());
            new BelongToProjectMap(builder.Entity<BelongToProject>());
            base.OnModelCreating(builder); 
        } 
   }
}