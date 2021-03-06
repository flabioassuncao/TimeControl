using TimeControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace TimeControl.Interfaces.Repository
{
    public interface IActivityRepository
   {
      void Add(Activity activity);
      IEnumerable<Activity> GetAll();
      IEnumerable<Activity> GetAllUser(Guid userId);
      IEnumerable<Activity> GetAllProject(Guid projectId);
      Activity Find(Guid Id);
      void Remove(Guid Id);
      void Update([FromBody] Activity activity);
   }
}