using Microsoft.Data.Entity.Metadata.Builders;
using TimeControl.Models;

namespace Infra.Map
{
   public class ApplicationUserMap
   {
       public ApplicationUserMap(EntityTypeBuilder<ApplicationUser> entityBuilder)
       {
        //    entityBuilder.HasOne(x => x.UserProject)
        //    .WithOne(x => x.Administrator)
        //    .HasForeignKey<UsersProjects>(x => x.AdministratorId);
       }
   }
}