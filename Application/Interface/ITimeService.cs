using System;
using TimeControl.Models;

namespace TimeControl.Application.Interface
{
    public interface ITimeService
    {
        Time SaveTime(Time time);
        void UpdateTime(Time time);
        void DeleteTime(Guid Id);
    }
}