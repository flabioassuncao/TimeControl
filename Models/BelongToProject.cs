using System;

namespace TimeControl.Models
{
    public class BelongToProject
   {
    //    public Guid Id { get; set; }
       public Guid ProjectId { get; set; }
       public Project Project { get; set; }
       public Guid MemberId { get; set; }
       public User Member { get; set; }
   }
}