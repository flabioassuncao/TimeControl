using System;

namespace TimeControl.Models
{
   public class Time
   {
       public Guid TimeId { get; set; }
       public DateTime StartDate  { get; set; }
       public DateTime EndDate { get; set; }
       public string ActivityTime { get; set; }
       public bool Status { get; set; }
       public Guid ActivityId { get; set; }
       public virtual Activity Activity { get; set; }
   }
}