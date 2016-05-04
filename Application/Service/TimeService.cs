using System;
using TimeControl.Application.Interface;
using TimeControl.Interfaces.Repository;
using TimeControl.Models;

namespace TimeControl.Service.Application
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;
        
        public TimeService(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public void DeleteTime(Guid Id)
        {
            _timeRepository.DeleteTime(Id);
        }

        public Time SaveTime(Time time)
        {
            time.TimeId = Guid.NewGuid();
            _timeRepository.SaveTime(time);
            return time;
        }

        public void UpdateTime(Time time)
        {
            _timeRepository.UpdateTime(time);
        }
    }
}