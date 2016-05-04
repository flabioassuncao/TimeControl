using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using TimeControl.Application.Interface;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Service.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Add(User user)
        {
            user.UserId = Guid.NewGuid();
            _userRepository.Add(user);
            return user;
        }

        public User Find(string userName)
        {
            return _userRepository.Find(userName);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<User> GetAllBelongProject(Guid projectId)
        {
            return _userRepository.GetAllBelongProject(projectId);
        }

        public IEnumerable<User> GetAllNames()
        {
            return _userRepository.GetAllNames();
        }

        public void Remove(Guid Id)
        {
            _userRepository.Remove(Id);
        }

        public void Update([FromBody]User user)
        {
            _userRepository.Update(user);
        }
    }
}