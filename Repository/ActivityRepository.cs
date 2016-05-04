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
            return _context.Activities.AsNoTracking()
                    .Include(a => a.Times)
                    .FirstOrDefault(a => a.ActivityId.Equals(Id));
        }

        public IEnumerable<Activity> GetAll()
        {
            return _context.Activities.AsNoTracking()
                    .Include(a => a.Times)
                    .Include(a => a.Project)
                    .Include(a => a.Responsible);
        }

        public void Remove(Guid Id)
        {
            var entity = _context.Activities.First(t => t.ActivityId == Id);
            _context.Activities.Remove(entity);
            _context.SaveChanges();
        }

        public void Update([FromBody] Activity activity)
        {
            var itemToUpdate = _context.Activities.SingleOrDefault(r => r.ActivityId == activity.ActivityId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Observation = activity.Observation;
                itemToUpdate.ResponsibleId = activity.ResponsibleId;
                itemToUpdate.LastTimeWorked = activity.LastTimeWorked;
                itemToUpdate.ResponsibleId  = activity.ResponsibleId;
                itemToUpdate.ProjectId = activity.ProjectId;
                
            }
             _context.SaveChanges();
        }

        public IEnumerable<Activity> GetAllUser(Guid userId)
        {
            return _context.Activities.AsNoTracking()
                    .Include(a => a.Times)
                    .Include(a => a.Project)
                    .Include(a => a.Responsible)
                    .Where(x => x.Responsible.UserId == userId);
        }

        public IEnumerable<Activity> GetAllProject(Guid projectId)
        {
            return _context.Activities.AsNoTracking()
                    .Include(a => a.Times)
                    .Where(x => x.ProjectId == projectId);
        }
    }
}
