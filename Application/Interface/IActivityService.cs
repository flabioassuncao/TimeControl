using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Models;

namespace TimeControl.Application.Interface
{
    public interface IActivityService
    {
        Activity Add(Activity activity);
        IEnumerable<Activity> GetAll();
        IEnumerable<Activity> GetAllUser(string responsible);
        Activity Find(Guid Id);
        void Remove(Guid Id);
        void Update([FromBody] Activity activity);
        Time SaveTime(Time time);
        void UpdateTime(Time time);
        void DeleteTime(Guid Id);
        
    }
}