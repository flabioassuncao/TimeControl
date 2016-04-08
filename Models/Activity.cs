using System;
using System.Collections.Generic;

namespace TimeControl.Models
{
   public class Activity
   {
       public Guid activityId { get; set; }
       public string Observation { get; set; }
       public string Link { get; set; }
       public bool Status { get; set; }
       public IList<Time> Times { get; set; }
       public string Responsible { get; set; }
       public Guid ResponsibleId { get; set; }
   }
}