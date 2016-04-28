using TimeControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace TimeControl.Interfaces.Repository
{
    public interface IUserRepository
   {
       void Add(User user);
      IEnumerable<User> GetAll();
      IEnumerable<User> GetAllNames();
      IEnumerable<User> GetAllBelongProject(Guid projectId);
      IEnumerable<Project> GetProjectsParticipating(Guid userId);
      User Find(Guid Id);
      void Remove(Guid Id);
      void Update([FromBody] User user);
   }
}