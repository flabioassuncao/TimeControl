using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using TimeControl.Models;

namespace Infra.Map
{
   public class UsersProjectsMap
   {
       public UsersProjectsMap(EntityTypeBuilder<UsersProjects> entityBuilder)
       {
        //    entityBuilder.HasOne(x => x.BelongToProject)
        //    .WithOne(x => x.Project)
        //    .HasForeignKey<BelongToProject>(x => x.ProjectId);
       }
   }
}