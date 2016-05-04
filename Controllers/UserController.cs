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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {   
            return Json(_userService.Add(user));
        }
        
        [HttpGet]
        public IEnumerable<User>GetAll()
        {            
            return _userService.GetAll();
        }
        
        [HttpGet("{userName}", Name = "GetUser")]
        public IActionResult GetById(string userName)
        {
            return new ObjectResult(_userService.Find(userName));
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        public IEnumerable<User>GetAllUser()
        {            
            return _userService.GetAllNames();
        }
        
        [HttpGet]
        [Route("GetAllBelongProject/{projectId}")]
        public IEnumerable<User>GetAllBelongProject(Guid projectId)
        {
            return _userService.GetAllBelongProject(projectId);
        }
    }
}