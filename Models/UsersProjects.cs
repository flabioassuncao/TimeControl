using System;

namespace TimeControl.Models
{
    public class UsersProjects
   {
       public Guid Id { get; set; }
       public Guid AdministratorId { get; set; }
       public User Administrator { get; set; }
       public Guid ProjectId { get; set; }
       public Project Project { get; set; }
       
       public BelongToProject BelongToProject { get; set; }
   }
}