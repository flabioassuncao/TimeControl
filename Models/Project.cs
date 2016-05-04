using System;
using System.Collections.Generic;

namespace TimeControl.Models
{
    public class Project
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public IList<Activity> Activities { get; set; }
        public IList<BelongToProject> BelongToProject { get; set; }
        public Guid AdministratorId { get; set; }
        public virtual User Administrator { get; set; }
    }
}