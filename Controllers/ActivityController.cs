using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Models;
using TimeControl.Repository;

namespace TimeControl.Controllers
{
    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        
        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        
        [HttpGet]
        public IEnumerable<Activity>GetAll()
        {            
            return _activityRepository.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetActivity")]
        public IActionResult GetById(Guid Id)
        {
            return new ObjectResult(_activityRepository.Find(Id));
        }
        
        [HttpPost]
        public void Create([FromBody] Activity activity)
        {
            _activityRepository.Add(activity);
        }
 
        [HttpPut("{id}")]
        public void Update(Guid id, [FromBody]Activity activity)
        {
            _activityRepository.Update(id, activity);
        }
 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _activityRepository.Remove(id);
        }
    }
}