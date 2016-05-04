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
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;
        
        public UserRepository(DataBaseContext context) 
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User Find(string userName)
        {
            return _context.User.AsNoTracking()
                    .Include(a => a.BelongToProject)
                    .ThenInclude(a => a.Member)
                    .Include(a => a.ListProjectsAdmin)
                    .ThenInclude(a => a.Activities)
                    .ThenInclude(a => a.Times)
                    .FirstOrDefault(a => a.UserName.Equals(userName));
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.AsNoTracking()
                    .Include(a => a.BelongToProject)
                    .ThenInclude(a => a.Member)
                    .Include(a => a.ListProjectsAdmin)
                    .ThenInclude(a => a.Activities)
                    .ThenInclude(a => a.Times);
        }

        public IEnumerable<User> GetAllBelongProject(Guid projectId)
        {
            return _context.User.AsNoTracking()
                    .Where(x => x.BelongToProject.Any(c => c.ProjectId == projectId));
        }

        public IEnumerable<User> GetAllNames()
        {
            return _context.User.AsNoTracking();
        }

        public void Remove(Guid Id)
        {
            var entity = _context.User.First(t => t.UserId == Id);
            _context.User.Remove(entity);
            _context.SaveChanges();
        }

        public void Update([FromBody]User user)
        {
            var itemToUpdate = _context.User.SingleOrDefault(r => r.UserId == user.UserId);
            if (itemToUpdate != null)
            {
                itemToUpdate.UserName = user.UserName;
            }
             _context.SaveChanges();
        }
    }
}