
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using TimeControl.Models;

namespace TimeControl.Repository
{
    public class ResponsibleRepository : IResponsibleRepository
    {
        private readonly DataBaseContext _context;

        private readonly ILogger _logger;
        
        public ResponsibleRepository(DataBaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("IResponsibleRepository");
        }
        public void Add(Responsible responsible)
        {
            _context.Responsibles.Add(responsible);
            _context.SaveChanges();
        }

        public IEnumerable<Responsible> GetAll()
        {
            _logger.LogCritical("Getting a the existing records");
            return _context.Responsibles.ToList();
        }

        public Responsible Find(Guid Id)
        {
            return _context.Responsibles.First(t => t.responsibleId == Id);
        }

        public void Remove(Guid Id)
        {
            var entity = _context.Responsibles.First(t => t.responsibleId == Id);
            _context.Responsibles.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Guid id, [FromBody]Responsible responsible)
        {
            _context.Responsibles.Update(responsible);
            _context.SaveChanges();
        }
    }
}