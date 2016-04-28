using System;
using System.Collections.Generic;

namespace TimeControl.Models
{
   public class Activity
   {
       public Guid ActivityId { get; set; }
       public string Observation { get; set; }
       public string Link { get; set; }
       public bool Status { get; set; }
       public DateTime LastTimeWorked { get; set; }
       public IList<Time> Times { get; set; }
       public Guid ResponsibleId { get; set; }
       public User Responsible { get; set; }
       public Guid ProjectId { get; set; }
       public virtual Project Project { get; set; }
       
   }
}