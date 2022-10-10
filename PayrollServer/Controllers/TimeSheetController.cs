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
        //public IEnumerable<IGrouping<int,TimeSheet>> GetAllTimePeriods()
        public IEnumerable<TimeSheet> GetAllTimePeriods()
        {
            //var result = _repository.TimeSheets.AsEnumerable().GroupBy(r => r.SheetId).ToList();
            var result = _repository.TimeSheets.ToList();
            
            return result;
        }

        [HttpPost]
        public void Insert(List<TimeSheet> timeSheets)
        {
            var last = _repository.TimeSheets.Where(r => r.DateDeleted == null)
                                            .OrderByDescending(r => r.SheetId).FirstOrDefault();
            int lastId = 1;
            if(last != null)
            {
                lastId = last.SheetId;
            }

            lastId++;
            foreach (var item in timeSheets)
            {
                item.Id = Guid.NewGuid();
                item.SheetId = lastId;
                item.DateCreated = DateTime.Now;
            }

            _repository.TimeSheets.AddRange(timeSheets);
            _repository.SaveChanges();
        }
    }
}
