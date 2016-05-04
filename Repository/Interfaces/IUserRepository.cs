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
      User Find(string userName);
      void Remove(Guid Id);
      void Update([FromBody] User user);
   }
}