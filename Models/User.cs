using System;
using System.Collections.Generic;

namespace TimeControl.Models
{
    public class User
   {
       public Guid UserId { get; set; }
       public string UserName { get; set; }
       public IList<BelongToProject> BelongToProject { get; set; }
       public IList<Project> ListProjectsAdmin { get; set; }
       public IList<Activity> Activities { get; set; }
   }
}