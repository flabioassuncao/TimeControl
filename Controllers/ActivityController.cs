using System;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Controllers
{
    // [Authorize]
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
        
        [HttpGet]
        [Route("GetAllUser/{user}")]
        public IEnumerable<Activity>GetAllUser(string user)
        {            
            return _activityRepository.GetAllUser(user);
        }
        
        
        [HttpGet("{id}", Name = "GetActivity")]
        public IActionResult GetById(Guid Id)
        {
            return new ObjectResult(_activityRepository.Find(Id));
        }
        
        
        [HttpGet("{user}", Name = "GetActivity2")]
         public IActionResult getByUser(string user)
        {
            try{
                return new ObjectResult(_activityRepository.Find(user));
            }catch{
                return HttpBadRequest();
            }
        }
        
        [HttpPost]
        public void Create([FromBody] Activity activity)
        {
            _activityRepository.Add(activity);
        }
 
        [HttpPut]
        public void Update([FromBody]Activity activity)
        {
            _activityRepository.Update(activity);
        }
 
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _activityRepository.Remove(id);
        }
        
        // POST api/[controller]/SaveTime
        [HttpPost]
        [Route("SaveTime")]
        public void SaveTime([FromBody] Time time)
        {
            _activityRepository.SaveTime(time);
        }
        
        // PUT api/[controller]/UpdateTime
        [HttpPut]
        [Route("UpdateTime")]
        public void UpdateTime([FromBody]Time time)
        {
            _activityRepository.UpdateTime(time);
        }
        
        // DELETE api/[controller]/DeleteTime
        [HttpDelete]
        [Route("DeleteTime/{id:Guid}")]
        public void DeleteTime(Guid id)
        {
            Console.WriteLine("entrou no delete");
            _activityRepository.DeleteTime(id);
        }
        
    }
}