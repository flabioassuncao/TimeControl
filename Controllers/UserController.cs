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
        
        [HttpGet]
        public IEnumerable<User>GetAll()
        {            
            return _userService.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(Guid Id)
        {
            return new ObjectResult(_userService.Find(Id));
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
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>CONTROLLER<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            return _userService.GetAllBelongProject(projectId);
        }
        
        [HttpGet]
        [Route("GetProjectsParticipating/{userId}")]
        public IEnumerable<Project>GetProjectsParticipating(Guid userId)
        {
            return _userService.GetProjectsParticipating(userId);
        }
    }
}