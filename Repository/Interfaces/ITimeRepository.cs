using TimeControl.Models;
using System;

namespace TimeControl.Interfaces.Repository
{
    public interface ITimeRepository
   {
      void SaveTime(Time time);
      void UpdateTime(Time time);
      void DeleteTime(Guid Id);
   }
}