using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Application.Interface;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Service.Application
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        
        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        
        public Activity Add(Activity activity)
        {
            activity.ActivityId = Guid.NewGuid();
            _activityRepository.Add(activity);
            return activity;
        }

        public Activity Find(Guid Id)
        {
            return _activityRepository.Find(Id);
        }

        public IEnumerable<Activity> GetAll()
        {
            return _activityRepository.GetAll();
        }

        public IEnumerable<Activity> GetAllProject(Guid projectId)
        {
            return _activityRepository.GetAllProject(projectId);
        }

        public IEnumerable<Activity> GetAllUser(Guid UserId)
        {
            return _activityRepository.GetAllUser(UserId);
        }

        public void Remove(Guid Id)
        {
            _activityRepository.Remove(Id);
        }

        public void Update([FromBody]Activity activity)
        {
            _activityRepository.Update(activity);
        }
    }
}