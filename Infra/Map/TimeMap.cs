using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using TimeControl.Models;

namespace Infra.Map
{
   public class TimeMap
   {
       public TimeMap(EntityTypeBuilder<Time> entityBuilder)
       {
           entityBuilder.HasKey(x => x.TimeId);
           entityBuilder.Property(x => x.StartDate).IsRequired();
           entityBuilder.Property(x => x.EndDate).IsRequired();
           entityBuilder.Property(x => x.ActivityId).IsRequired();
           
           entityBuilder.HasOne(x => x.Activity).WithMany(x => x.Times).OnDelete(DeleteBehavior.Restrict);    
       }
   }
}