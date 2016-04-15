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
            activity.activityId = Guid.NewGuid();
            _activityRepository.Add(activity);
            return activity;
        }

        public void DeleteTime(Guid Id)
        {
            _activityRepository.DeleteTime(Id);
        }

        public Activity Find(Guid Id)
        {
            return _activityRepository.Find(Id);
        }

        public IEnumerable<Activity> GetAll()
        {
            return _activityRepository.GetAll();
        }

        public IEnumerable<Activity> GetAllUser(string responsible)
        {
            return _activityRepository.GetAllUser(responsible);
        }

        public void Remove(Guid Id)
        {
            _activityRepository.Remove(Id);
        }

        public Time SaveTime(Time time)
        {
            time.TimeId = Guid.NewGuid();
            _activityRepository.SaveTime(time);
            return time;
        }

        public void Update([FromBody]Activity activity)
        {
            _activityRepository.Update(activity);
        }

        public void UpdateTime(Time time)
        {
            _activityRepository.UpdateTime(time);
        }
    }
}