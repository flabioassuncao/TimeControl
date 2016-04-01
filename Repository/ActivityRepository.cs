
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
            // return ActivityList.Find(m => m.activityId.Equals(key));
            return _context.Activities.AsNoTracking().First(t => t.activityId == Id);
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
            return _context.Activities.AsNoTracking().ToList();
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
                itemToUpdate.Status = activity.Status;
                itemToUpdate.Time = activity.Time;
                itemToUpdate.StartDate = activity.StartDate;
                itemToUpdate.EndDate = activity.EndDate;
                itemToUpdate.Responsible = activity.Responsible;
                itemToUpdate.ResponsibleId = activity.ResponsibleId;
            }
             _context.SaveChanges();
        }

        
    }
}
