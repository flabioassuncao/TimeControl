
using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Data.Context;
using Microsoft.AspNet.Mvc;
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
            return _context.Activities.First(t => t.activityId == Id);
        }

        public IEnumerable<Activity> GetAll()
        {
            _logger.LogCritical("Getting a the existing records");
            return _context.Activities.ToList();
        }

        public void Remove(Guid Id)
        {
            var entity = _context.Activities.First(t => t.activityId == Id);
            _context.Activities.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Guid Id, [FromBody] Activity activity)
        {
             _context.Activities.Update(activity);
            _context.SaveChanges();
        }

        
    }
}
