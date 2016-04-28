using TimeControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace TimeControl.Interfaces.Repository
{
    public interface IProjectRepository
   {
      void Add(Project project);
      IEnumerable<Project> GetAll();
      
      IEnumerable<Project> GetAllNames();
      
    //   IEnumerable<Project> GetAllUser(string administrator);
      Project Find(Guid Id);
      void Remove(Guid Id);
      void Update([FromBody] Project project);
      
      void AddBelongTable(BelongToProject ids);
   }
}