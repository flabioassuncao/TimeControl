using TimeControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace TimeControl.Repository
{
    public interface IResponsibleRepository
   {
      void Add(Responsible responsible);
      IEnumerable<Responsible> GetAll();
      Responsible Find(Guid Id);
      void Remove(Guid Id);
      void Update(Guid id, [FromBody] Responsible responsible);
   }
}