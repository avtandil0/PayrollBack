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
        public IEnumerable<TimePeriod> GetAllDepartments()
        {
            var result = _repository.TimePeriods.ToList();
            return result;
        }

        [HttpPost]
        public void Insert(List<TimePeriod> timePeriods)
        {
            foreach (var item in timePeriods)
            {
                item.Id = Guid.NewGuid();
                item.DateCreated = DateTime.Now;
            }
            _repository.TimePeriods.AddRange(timePeriods);
            _repository.SaveChanges();
        }

    }
}
