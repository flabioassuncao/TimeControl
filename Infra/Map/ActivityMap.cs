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
           
       }
   }
}