using TimeControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace TimeControl.Repository
{
    public interface IActivityRepository
   {
      void Add(Activity activity);
      IEnumerable<Activity> GetAll();
      Activity Find(Guid Id);
      void Remove(Guid Id);
      void Update(Guid id, [FromBody] Activity activity);
   }
}