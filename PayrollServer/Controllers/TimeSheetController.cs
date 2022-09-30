using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private RepositoryContext _repository;
        public TimeSheetController(RepositoryContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<TimeSheet> GetAllTimePeriods()
        {
            var result = _repository.TimeSheets.ToList();
            return result;
        }

        [HttpPost]
        public void Insert(List<TimeSheet> timeSheets)
        {
            
            _repository.SaveChanges();
        }
    }
}
