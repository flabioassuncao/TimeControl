
using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Data.Context;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DataBaseContext _context;
        
        private readonly ILogger _logger;
        static List<Activity> ActivityList = new List<Activity>();
        
        public ActivityRepository(DataBaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("IActivityRepository");   
        }
        
        public void Add(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        public Activity Find(Guid Id)
        {            
            var activities = _context.Activities.AsNoTracking().First(t => t.activityId == Id);
            var times      = _context.Times.Where(x => x.activityId == activities.activityId).ToList();
            activities.Times = times;
            return activities;
        }
        
        public Activity Find(string user)
        {
            // return ActivityList.Find(m => m.activityId.Equals(key));
            return _context.Activities.AsNoTracking().First(t => t.Responsible == user);
        }

        public IEnumerable<Activity> GetAll()
        {
            _logger.LogCritical("Getting a the existing records");
            // return _context.Activities.AsNoTracking().ToList();
            
            
            return _context.Activities.AsNoTracking()
            .Select(a => new Activity()
            {
                activityId = a.activityId,
                Observation = a.Observation,
                Link = a.Link,
                Responsible = a.Responsible,
                Times = a.Times.Where(t => t.activityId == a.activityId).ToList()
            })
            .ToList<Activity>();
        }

        public void Remove(Guid Id)
        {
            var entity = _context.Activities.First(t => t.activityId == Id);
            _context.Activities.Remove(entity);
            _context.SaveChanges();
        }

        public void Update([FromBody] Activity activity)
        {
            var itemToUpdate = _context.Activities.SingleOrDefault(r => r.activityId == activity.activityId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Observation = activity.Observation;
                itemToUpdate.Link = activity.Link;
                itemToUpdate.Responsible = activity.Responsible;
                itemToUpdate.ResponsibleId = activity.ResponsibleId;
            }
             _context.SaveChanges();
        }

        public void SaveTime(Time time)
        {
             _context.Times.Add(time);
            _context.SaveChanges();
        }

        public void UpdateTime(Time time)
        {
            var itemToUpdate = _context.Times.SingleOrDefault(t => t.TimeId == time.TimeId);
            if (itemToUpdate != null)
            {
                itemToUpdate.ActivityTime = time.ActivityTime;
                itemToUpdate.StartDate = time.StartDate;
                itemToUpdate.EndDate = time.EndDate;
                itemToUpdate.status = time.status;
            }
            _context.SaveChanges();
        }

        public void DeleteTime(Guid Id)
        {
            var entity = _context.Times.First(t => t.TimeId == Id);
            _context.Times.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Activity> GetAllUser(string responsible)
        {
            _logger.LogCritical("Getting a the existing records");
            // return _context.Activities.AsNoTracking().ToList();
            
            
            return _context.Activities.AsNoTracking()
            .Select(a => new Activity()
            {
                activityId = a.activityId,
                Observation = a.Observation,
                Link = a.Link,
                Responsible = a.Responsible,
                Times = a.Times.Where(t => t.activityId == a.activityId).ToList()
            })
            .Where(r => r.Responsible == responsible)
            .ToList<Activity>();
        }
    }
}
