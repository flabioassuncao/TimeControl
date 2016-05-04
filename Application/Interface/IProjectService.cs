using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Models;

namespace TimeControl.Application.Interface
{
    public interface IProjectService
    {
      Project Add(Project project);
      IEnumerable<Project> GetAll(Guid userId);
      IEnumerable<Project> GetAllNamesProjects(Guid UserId);
      IEnumerable<Project> GetProjectsParticipating(Guid userId);
      bool AddBelongTable(BelongToProject ids);
      Project Find(Guid Id);
      void Remove(Guid Id);
      void Update([FromBody] Project project);
      void DeleteBelongTable(BelongToProject ids);
    }
}