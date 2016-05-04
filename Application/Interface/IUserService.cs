using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Models;

namespace TimeControl.Application.Interface
{
    public interface IUserService
    {
      User Add(User user);
      IEnumerable<User> GetAll();
      IEnumerable<User> GetAllNames();
      IEnumerable<User> GetAllBelongProject(Guid projectId);
      User Find(string userName);
      void Remove(Guid Id);
      void Update([FromBody] User user);
    }
}