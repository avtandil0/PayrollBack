using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    //[ApiController]
    public class TimeTableController : ControllerBase
    {
        private RepositoryContext _repository;
        public TimeTableController(RepositoryContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllTimePeriods")]
        public IEnumerable<TimePeriod> GetAllTimePeriods(Guid employeeId)
        {
            if (employeeId == null)
            {
                return _repository.TimePeriods.Where(r => r.DateDeleted == null).ToList();
            }

            return _repository.TimePeriods.Where(r => r.DateDeleted == null && r.EmployeeId == employeeId).ToList();
        }
        [HttpGet]
        [Route("GetAllTimePeriodsById")]
        public IEnumerable<TimePeriod> GetAllTimePeriodsById(Guid employeeId)
        {
            var result = _repository.TimePeriods.Where(r => r.DateDeleted == null && r.EmployeeId == employeeId).ToList();
            return result;
        }

        public class InsertParams
        {
            public List<TimePeriod> timePeriods { get; set; }
            public List<string> range { get; set; }
            public Guid EmployeeId { get; set; }
            public List<Guid> TreeValues { get; set; }
        }
        [HttpPost]
        public void Insert([FromBody] InsertParams insertParams)
        {
            var timePeriods = insertParams.timePeriods;
            var range = insertParams.range;
            var employeeId = insertParams.EmployeeId;
            var treeValues = insertParams.TreeValues;
            var employees = new List<Guid>();
            if (treeValues != null && treeValues.Count > 0)
            {
                foreach (var emp in treeValues)
                {
                    var exist = _repository.Employees.FirstOrDefault(r => r.Id == emp);
                    if( exist != null)
                    {
                        employees.Add(emp);
                    }
                    else
                    {
                        var emps = _repository.Employees.Where(r => r.DepartmentId == emp && r.DateDeleted == null)
                                            .Select(k => k.Id);
                        employees.AddRange(emps);

                    }
                }
            }

            var currentTime = DateTime.Now;
            var newPeriods = new List<TimePeriod>();

            if (range.Count > 0)
            {
                DateTime rangeStartDate = DateTime.Parse(range[0]);
                DateTime rangeEndDate = DateTime.Parse(range[1]);
                DateTime rangeItemDate = rangeStartDate;

                while (rangeStartDate.AddDays(1) <= rangeEndDate)
                {
                    foreach (var item in timePeriods)
                    {
                        if(employees.Count > 0)
                        {
                            foreach (var emp in employees)
                            {
                                var newTimePeriod = new TimePeriod();
                                newTimePeriod.Id = Guid.NewGuid();
                                newTimePeriod.DateCreated = currentTime;
                                newTimePeriod.Date = rangeStartDate;
                                newTimePeriod.StartTime = item.StartTime;
                                newTimePeriod.EndTime = item.EndTime;
                                newTimePeriod.EmployeeId = emp;
                                newPeriods.Add(newTimePeriod);
                            }
                        }
                        else
                        {
                            var newTimePeriod = new TimePeriod();
                            newTimePeriod.Id = Guid.NewGuid();
                            newTimePeriod.DateCreated = currentTime;
                            newTimePeriod.Date = rangeStartDate;
                            newTimePeriod.StartTime = item.StartTime;
                            newTimePeriod.EndTime = item.EndTime;
                            newTimePeriod.EmployeeId = employeeId;
                            newPeriods.Add(newTimePeriod);
                        }
                        

                    }

                    rangeStartDate = rangeStartDate.AddDays(1);
                }


            }
            else
            {
                foreach (var item in timePeriods)
                {
                    var exist = _repository.TimePeriods.FirstOrDefault(r => r.Id == item.Id);
                    if (exist != null)
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
                        newTimePeriod.EmployeeId = employeeId;
                        newPeriods.Add(newTimePeriod);
                    }

                }

            }


            if (newPeriods.Count > 0)
            {
                _repository.TimePeriods.AddRange(newPeriods);
            }
            _repository.SaveChanges();
        }

    }
}
