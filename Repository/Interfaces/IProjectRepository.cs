using TimeControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace TimeControl.Interfaces.Repository
{
    public interface IProjectRepository
   {
      void Add(Project project);
      IEnumerable<Project> GetAll(Guid userId);
      IEnumerable<Project> GetAllNamesProjects(Guid UserId);
      IEnumerable<Project> GetProjectsParticipating(Guid userId);
      Project Find(Guid Id);
      void Remove(Guid Id);
      void Update([FromBody] Project project);
      bool AddBelongTable(BelongToProject ids);
      void DeleteBelongTable(BelongToProject ids);
   }
}