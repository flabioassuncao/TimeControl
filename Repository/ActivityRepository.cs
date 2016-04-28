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
        
        // static List<Activity> ActivityList = new List<Activity>();
        
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
                    .FirstOrDefault(a => a.ActivityId.Equals(Id));
        }

        public IEnumerable<Activity> GetAll()
        {
            return _context.Activities
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
            Console.WriteLine("SHUAHUSHAUHSUSHUSHSUSHUSHSUHSUHAUHASUHUSHAUSHAUSHSUHUAHSUASHUASHUASHAUSH");
            Console.WriteLine(activity);
            var itemToUpdate = _context.Activities.SingleOrDefault(r => r.ActivityId == activity.ActivityId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Observation = activity.Observation;
                itemToUpdate.Responsible = activity.Responsible;
                itemToUpdate.LastTimeWorked = activity.LastTimeWorked;
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
                itemToUpdate.Status = time.Status;
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
                    .Include(a => a.Times)
                    .Include(a => a.Project)
                    .Include(a => a.Responsible)
                    // .Where(x => x.Responsible.UserId == Guid.Parse("fe384930-b71a-4013-9694-1f48bc436fb0"));
                    .Where(x => x.Responsible.UserName == "Flabio");
        }

        public IEnumerable<Activity> GetAllProject(Guid projectId)
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>REPOSITORY<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine(projectId);
            return _context.Activities
                    .Include(a => a.Times)
                    .Where(x => x.ProjectId == projectId);
        }
    }
}
