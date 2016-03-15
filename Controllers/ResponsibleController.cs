using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Models;
using TimeControl.Repository;

namespace TimeControl.Controllers
{
    [Route("api/[controller]")]
    public class ResponsibleController : Controller
    {
        private readonly IResponsibleRepository _responsibleRepository;
        
        public ResponsibleController(IResponsibleRepository responsibleRepository)
        {
            _responsibleRepository = responsibleRepository;
        }
        
        [HttpGet]
        public IEnumerable<Responsible>GetAll()
        {            
            return _responsibleRepository.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetResponsible")]
        public IActionResult GetById(Guid Id)
        {
            return new ObjectResult(_responsibleRepository.Find(Id));
        }
        
        [HttpPost]
        public void Create([FromBody] Responsible responsible)
        {
            _responsibleRepository.Add(responsible);
        }
 
        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]Responsible responsible)
        {
            _responsibleRepository.Update(id, responsible);
        }
 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _responsibleRepository.Remove(id);
        }
        
        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Route("index2")]
        public IActionResult Index2()
        {
            return View();
        }
        
        [HttpGet]
        [Route("index3")]
        public IActionResult Index3()
        {
            return View();
        }
        
        [HttpGet]
        [Route("index4")]
        public IActionResult Index4()
        {
            return View();
        }
    }
}