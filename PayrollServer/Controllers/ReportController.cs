using Entities;
using Entities.FilterModels;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollServer.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ReportController : ControllerBase
    {
        private RepositoryContext _repository;
        public ReportController(RepositoryContext repository)
        {
            _repository = repository;
        }

       
        [HttpGet]
        [Route("getReport")]
        public IEnumerable<Employee> GetReport(CalculationFilter calculationFilter)
        {
            var query = _repository.Employees.Where(r => r.DateDeleted == null);

            if (!string.IsNullOrEmpty(calculationFilter.FirstName))
            {
                query = query.Where(r => r.FirstName.Contains(calculationFilter.FirstName));
            }

            if (!string.IsNullOrEmpty(calculationFilter.LastName))
            {
                query = query.Where(r => r.FirstName.Contains(calculationFilter.LastName));
            }

            return query.Include(r => r.EmployeeComponents.Where(k => k.DateDeleted == null))
                            .ThenInclude(r => r.Component).Where(d => d.DateDeleted == null).ToList();
        }
       

    }
}
