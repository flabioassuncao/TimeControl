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

        public bool AddBelongTable(BelongToProject ids)
        {
                     
            return _projectRepository.AddBelongTable(ids);
        }

        public void DeleteBelongTable(BelongToProject ids)
        {
            _projectRepository.DeleteBelongTable(ids);
        }

        public Project Find(Guid Id)
        {
            return _projectRepository.Find(Id);
        }

        public IEnumerable<Project> GetAll(Guid userId)
        {
            return _projectRepository.GetAll(userId);
        }

        public IEnumerable<Project> GetAllNamesProjects(Guid UserId)
        {
            return _projectRepository.GetAllNamesProjects(UserId);
        }

        public IEnumerable<Project> GetProjectsParticipating(Guid userId)
        {
            return _projectRepository.GetProjectsParticipating(userId);
        }

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