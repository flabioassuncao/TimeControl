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
      Activity Find(Guid Id);
    //   Activity Find(string user, bool status);
      Activity Find(string user);
      void Remove(Guid Id);
      void Update([FromBody] Activity activity);
      
   }
}