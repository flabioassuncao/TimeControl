using System;

namespace TimeControl.Models
{
    public class BelongToProject
   {
       public Guid ProjectId { get; set; }
       public virtual Project Project { get; set; }
       public Guid MemberId { get; set; }
       public virtual User Member { get; set; }
   }
}