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
        IEnumerable<Activity> GetAllUser(Guid UserId);
        IEnumerable<Activity> GetAllProject(Guid projectId);
        Activity Find(Guid Id);
        void Remove(Guid Id);
        void Update([FromBody] Activity activity);
    }
}