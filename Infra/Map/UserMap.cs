using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using TimeControl.Models;

namespace Infra.Map
{
    public class UserMap
   {
       public UserMap(EntityTypeBuilder<User> entityBuilder)
       {
           entityBuilder.HasKey(x => x.UserId);
           entityBuilder.Property(x => x.UserName).IsRequired();
           
           entityBuilder.HasMany(x => x.ListProjectsAdmin).WithOne(x => x.Administrator).OnDelete(DeleteBehavior.Restrict);
           entityBuilder.HasMany(x => x.Activities).WithOne(x => x.Responsible).OnDelete(DeleteBehavior.Restrict);
        //    entityBuilder.HasOne(x => x.BelongToProject).WithOne(x => x.Member).HasForeignKey<BelongToProject>(x => x.MemberId);
        //    entityBuilder.HasMany(x => x.ListProjectsAdmin).WithOne(x => x.Administrator).OnDelete(DeleteBehavior.Restrict);
       }
   }
}