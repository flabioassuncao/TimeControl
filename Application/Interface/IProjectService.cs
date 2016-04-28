using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Models;

namespace TimeControl.Application.Interface
{
    public interface IProjectService
    {
      Project Add(Project project);
      IEnumerable<Project> GetAll();
      IEnumerable<Project> GetAllNames();
      void AddBelongTable(BelongToProject ids);
    //   IEnumerable<Project> GetAllUser(string administrator);
      Project Find(Guid Id);
      void Remove(Guid Id);
      void Update([FromBody] Project project);
    }
}