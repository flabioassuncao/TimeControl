using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Application.Interface;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Service.Application
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        
        public Project Add(Project project)
        {
            project.ProjectId = Guid.NewGuid();
            _projectRepository.Add(project);
            return project;
        }

        public void AddBelongTable(BelongToProject ids)
        {
             _projectRepository.AddBelongTable(ids);
        }

        public Project Find(Guid Id)
        {
            return _projectRepository.Find(Id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public IEnumerable<Project> GetAllNames()
        {
            return _projectRepository.GetAllNames();
        }


        // public IEnumerable<Project> GetAllUser(string administrator)
        // {
        //     return _projectRepository.GetAllUser(administrator);
        // }

        public void Remove(Guid Id)
        {
            _projectRepository.Remove(Id);
        }

        public void Update([FromBody]Project project)
        {
            _projectRepository.Update(project);
        }
    }
}