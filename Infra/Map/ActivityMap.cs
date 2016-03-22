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
           entityBuilder.Property(x => x.Observation).IsRequired();
           entityBuilder.Property(x => x.Time).IsRequired();
           entityBuilder.Property(x => x.StartDate).IsRequired();
           entityBuilder.Property(x => x.EndDate).IsRequired();
           
       }
   }
}