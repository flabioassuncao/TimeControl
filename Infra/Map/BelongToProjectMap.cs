using Microsoft.Data.Entity.Metadata.Builders;
using TimeControl.Models;

namespace Infra.Map
{
   public class BelongToProjectMap
   {
       public BelongToProjectMap(EntityTypeBuilder<BelongToProject> entityBuilder)
       {
           entityBuilder.HasKey(t => new {t.ProjectId, t.MemberId});
           
           entityBuilder.HasOne(x => x.Project).WithMany(x => x.BelongToProject).HasForeignKey(x => x.ProjectId);
           entityBuilder.HasOne(x => x.Member).WithMany(x => x.BelongToProject).HasForeignKey(x => x.MemberId);
       }
   }
}