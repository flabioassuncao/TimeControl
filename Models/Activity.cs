using System;

namespace TimeControl.Models
{
   public class Activity
   {
       public Guid activityId { get; set; }
       public string Name { get; set; }
       public string Observation { get; set; }
       public string Link { get; set; }
       public string Time { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
   }
}