using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using TimeControl.Models;

namespace Infra.Map
{
   public class ActivityMap
   {
       public ActivityMap(EntityTypeBuilder<Activity> entityBuilder)
       {
           entityBuilder.HasKey(x => x.activityId);
           entityBuilder.Property(x => x.Link).IsRequired();
           
           entityBuilder.HasMany(x => x.Times).WithOne(x => x.Activity).OnDelete(DeleteBehavior.Restrict);       
       }
   }
}