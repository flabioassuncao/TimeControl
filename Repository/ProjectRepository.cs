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
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataBaseContext _context;
        
        public ProjectRepository(DataBaseContext context) 
        {
            _context = context;
        }
        
        public void Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void AddBelongTable(BelongToProject ids)
        {
            _context.BelongToProjects.Add(ids);
            _context.SaveChanges();
        }

        public Project Find(Guid Id)
        {
            return _context.Projects
                    .Include(a => a.Activities)
                    .FirstOrDefault(a => a.ProjectId.Equals(Id));
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects
                    .Include(a => a.Activities)
                    .ThenInclude(a => a.Times)
                    .Include(a => a.BelongToProject)
                    .ThenInclude(a => a.Member)
                    .Include(a => a.Administrator)
                    .Where(a =>a.Administrator.UserId == Guid.Parse("fe384930-b71a-4013-9694-1f48bc436fb0"));
        }

        public IEnumerable<Project> GetAllNames()
        {
            return _context.Projects;
        }


        // public IEnumerable<Project> GetAllUser(Project administrator)
        // {
        //     return _context.Projects
        //             .Include(a => a.Activities).Where(r => r.Administrator == administrator.Administrator);

        //             //ATENÇÃO AQUI...
        // }

        public void Remove(Guid Id)
        {
            var entity = _context.Projects.First(t => t.ProjectId == Id);
            _context.Projects.Remove(entity);
            _context.SaveChanges();
        }

        public void Update([FromBody]Project project)
        {
            var itemToUpdate = _context.Projects.SingleOrDefault(r => r.ProjectId == project.ProjectId);
            if (itemToUpdate != null)
            {
                itemToUpdate.ProjectName = project.ProjectName;
            }
             _context.SaveChanges();
        }
    }
}