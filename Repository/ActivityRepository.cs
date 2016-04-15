using System;
using System.Collections.Generic;
using System.Linq;
using Infra.Data.Context;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DataBaseContext _context;
        
        static List<Activity> ActivityList = new List<Activity>();
        
        public ActivityRepository(DataBaseContext context) 
        {
            _context = context;
        }
        
        public void Add(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        public Activity Find(Guid Id)
        {            
            return _context.Activities
                    .Include(a => a.Times)
                    .FirstOrDefault(a => a.activityId.Equals(Id));
        }

        public IEnumerable<Activity> GetAll()
        {
            return _context.Activities
                    .Include(a => a.Times);
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
            return _context.Activities
                    .Include(a => a.Times).Where(r => r.Responsible == responsible);
        }
    }
}
