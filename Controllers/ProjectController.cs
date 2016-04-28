using System;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TimeControl.Application.Interface;
using TimeControl.Models;

namespace TimeControl.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        [HttpGet]
        public IEnumerable<Project>GetAll()
        {            
            return _projectService.GetAll();
        }
        
        // [HttpGet]
        // [Route("GetAllUser/{user}")]
        // public IEnumerable<Project>GetAllUser(string user)
        // {            
        //     return _projectService.GetAllUser(user);
        // }
        
        [HttpGet("{id}", Name = "GetProject")]
        public IActionResult GetById(Guid Id)
        {
            return new ObjectResult(_projectService.Find(Id));
        }
        
        [HttpGet]
        [Route("GetAllProjects")]
        public IEnumerable<Project>GetAllUser()
        {            
            return _projectService.GetAllNames();
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Project project)
        {   
            return Json(_projectService.Add(project));
        }
        
        [HttpPut]
        public void Update([FromBody]Project project)
        {
            _projectService.Update(project);
        }
        
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _projectService.Remove(id);
        }
        
        [HttpPost]
        [Route("SaveBelong")]
        public void AddBelongProject([FromBody]BelongToProject proj)
        {
            _projectService.AddBelongTable(proj);    
        }
    }
}