using System;
using Microsoft.AspNet.Mvc;
using TimeControl.Application.Interface;
using TimeControl.Models;

namespace TimeControl.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    public class TimeController : Controller
    {
        private readonly ITimeService _timeService;
        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        [HttpPost]
        [Route("SaveTime")]
        public IActionResult SaveTime([FromBody] Time time)
        {
            return Json(_timeService.SaveTime(time));
        }
        
        [HttpPut]
        [Route("UpdateTime")]
        public void UpdateTime([FromBody]Time time)
        {
            _timeService.UpdateTime(time);
        }
        
        [HttpDelete]
        [Route("DeleteTime/{id:Guid}")]
        public void DeleteTime(Guid id)
        {
            _timeService.DeleteTime(id);
        }
    }
}