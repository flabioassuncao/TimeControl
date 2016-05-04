using System;
using System.Linq;
using Infra.Data.Context;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Repository
{
    public class TimeRepository : ITimeRepository
    {
        private readonly DataBaseContext _context;
        
        public TimeRepository(DataBaseContext context) 
        {
            _context = context;
        }

        public void DeleteTime(Guid Id)
        {
            var entity = _context.Times.First(t => t.TimeId == Id);
            _context.Times.Remove(entity);
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
    }
}