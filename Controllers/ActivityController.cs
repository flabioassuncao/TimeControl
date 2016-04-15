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
        [Route("GetAllUser/{user}")]
        public IEnumerable<Activity>GetAllUser(string user)
        {            
            return _activityService.GetAllUser(user);
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
        
        [HttpPost]
        [Route("SaveTime")]
        public IActionResult SaveTime([FromBody] Time time)
        {
            return Json(_activityService.SaveTime(time));
        }
        
        [HttpPut]
        [Route("UpdateTime")]
        public void UpdateTime([FromBody]Time time)
        {
            _activityService.UpdateTime(time);
        }
        
        [HttpDelete]
        [Route("DeleteTime/{id:Guid}")]
        public void DeleteTime(Guid id)
        {
            _activityService.DeleteTime(id);
        }
        
    }
}