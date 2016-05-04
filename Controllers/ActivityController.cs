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
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        
        [HttpGet]
        public IEnumerable<Activity>GetAll()
        {            
            return _activityService.GetAll();
        }
        
        [HttpGet]
        [Route("GetAllUser/{userId}")]
        public IEnumerable<Activity>GetAllUser(Guid userId)
        {
            return _activityService.GetAllUser(userId);
        }
        
        [HttpGet]
        [Route("GetAllProject/{projectId}")]
        public IEnumerable<Activity>GetAllProject(Guid projectId)
        {
            return _activityService.GetAllProject(projectId);
        }
        
        [HttpGet("{id}", Name = "GetActivity")]
        public IActionResult GetById(Guid Id)
        {
            return new ObjectResult(_activityService.Find(Id));
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Activity activity)
        {   
            return Json(_activityService.Add(activity));
        }
 
        [HttpPut]
        public void Update([FromBody]Activity activity)
        {
            _activityService.Update(activity);
        }
 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _activityService.Remove(id);
        }
    }
}