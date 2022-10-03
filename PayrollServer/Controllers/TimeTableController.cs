using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableController : ControllerBase
    {
        private RepositoryContext _repository;
        public TimeTableController(RepositoryContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<TimePeriod> GetAllTimePeriods()
        {
            var result = _repository.TimePeriods.Where(r => r.DateDeleted == null).ToList();
            return result;
        }

        [HttpPost]
        public void Insert(List<TimePeriod> timePeriods)
        {
            var currentTime = DateTime.Now;
            var newPeriods = new List<TimePeriod>();
            foreach (var item in timePeriods)
            {
                var exist = _repository.TimePeriods.FirstOrDefault(r => r.Id == item.Id);
                if(exist != null)
                {
                    exist.StartTime = item.StartTime;
                    exist.EndTime = item.EndTime;
                    exist.DateChange = currentTime;
                }
                else
                {
                    var newTimePeriod = new TimePeriod();
                    newTimePeriod.Id = Guid.NewGuid();
                    newTimePeriod.DateCreated = currentTime;
                    newTimePeriod.Date = item.Date;
                    newTimePeriod.StartTime = item.StartTime;
                    newTimePeriod.EndTime = item.EndTime;
                    newPeriods.Add(newTimePeriod);
                }
                
            }

            if(newPeriods.Count > 0)
            {
                _repository.TimePeriods.AddRange(newPeriods);
            }
            _repository.SaveChanges();
        }

    }
}
