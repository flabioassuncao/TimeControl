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
        [Route("GetAll/{userId}")]
        public IEnumerable<Project>GetAll(Guid userId)
        {            
            return _projectService.GetAll(userId);
        }
        
        [HttpGet("{id}", Name = "GetProject")]
        public IActionResult GetById(Guid Id)
        {
            return new ObjectResult(_projectService.Find(Id));
        }
        
        [HttpGet]
        [Route("GetAllNamesProjects/{userId}")]
        public IEnumerable<Project>GetAllNamesProjects(Guid UserId)
        {            
            return _projectService.GetAllNamesProjects(UserId);
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
        public IActionResult AddBelongProject([FromBody]BelongToProject proj)
        {
            var operatingResult = _projectService.AddBelongTable(proj);
            if(operatingResult)
                return Ok();
             else
                return HttpBadRequest();
        }
        
        [HttpPost]
        [Route("DeleteBelong")]
        public void DeleteBelongProject([FromBody]BelongToProject Ids)
        {
            _projectService.DeleteBelongTable(Ids);    
        }
        
        [HttpGet]
        [Route("GetProjectsParticipating/{userId}")]
        public IEnumerable<Project>GetProjectsParticipating(Guid userId)
        {
            return _projectService.GetProjectsParticipating(userId);
        }
        
    }
}