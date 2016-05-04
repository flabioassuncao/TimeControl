using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using TimeControl.Models;

namespace Infra.Map
{
   public class ProjectMap
   {
       public ProjectMap(EntityTypeBuilder<Project> entityBuilder)
       {
           entityBuilder.HasKey(x => x.ProjectId);
           entityBuilder.Property(x => x.ProjectName).IsRequired();
           
           entityBuilder.HasMany(x => x.Activities).WithOne(x => x.Project).OnDelete(DeleteBehavior.Restrict);
           entityBuilder.HasOne(x => x.Administrator).WithMany(x => x.ListProjectsAdmin).OnDelete(DeleteBehavior.Restrict);
       }
   }
}