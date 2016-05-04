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

        public bool AddBelongTable(BelongToProject ids)
        {
            var entity = _context.BelongToProjects.FirstOrDefault(t => t.MemberId == ids.MemberId && t.ProjectId == ids.ProjectId);
            if(entity == null){
                _context.BelongToProjects.Add(ids);
                _context.SaveChanges();
                return true;
            }else{
                return false;
            }
        }

        public void DeleteBelongTable(BelongToProject ids)
        {
            var entity = _context.BelongToProjects.FirstOrDefault(t => t.MemberId == ids.MemberId && t.ProjectId == ids.ProjectId);
            if(entity != null){
                _context.BelongToProjects.Remove(entity);
                _context.SaveChanges();
            }
        }

        public Project Find(Guid Id)
        {
            return _context.Projects.AsNoTracking()
                    .Include(a => a.Activities)
                    .FirstOrDefault(a => a.ProjectId.Equals(Id));
        }

        public IEnumerable<Project> GetAll(Guid userId)
        {
            return _context.Projects.AsNoTracking()
                    .Include(a => a.Activities)
                    .ThenInclude(a => a.Times)
                    .Include(a => a.BelongToProject)
                    .ThenInclude(a => a.Member)
                    .Include(a => a.Administrator)
                    .Where(a =>a.Administrator.UserId == userId);
        }

        public IEnumerable<Project> GetAllNamesProjects(Guid UserId)
        {
            return _context.Projects.AsNoTracking()
                    .Where(x => x.AdministratorId == UserId);
        }

        public IEnumerable<Project> GetProjectsParticipating(Guid userId)
        {
            return _context.Projects.AsNoTracking()
                    .Where(x => x.BelongToProject.Any(c => c.MemberId == userId));
        }

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
                 _context.SaveChanges();
            }
        }
    }
}