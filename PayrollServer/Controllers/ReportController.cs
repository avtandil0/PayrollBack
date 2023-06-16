using Entities;
using Entities.FilterModels;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            if(calculationFilter.DepartmentId != null && calculationFilter.DepartmentId.Count() > 0)
            {
                query = query.Where(r => calculationFilter.DepartmentId.Contains((Guid)r.DepartmentId));
            }

            if (calculationFilter.CalculationPeriod != null)
            {
                query = query.Where(r => r.Calculations
                            .Any(c => c.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
                                && c.PayrollYear == calculationFilter.CalculationPeriod.Value.Year));

                //query = query.Include(r => r.Calculations.Where(c => c.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
                //     && c.PayrollYear == calculationFilter.CalculationPeriod.Value.Year));
                //query = query.Include(r => r.Calculations).Where(c => c.Calculations
                //            .Where(k => k.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
                //                     && k.PayrollYear == calculationFilter.CalculationPeriod.Value.Year)));
            }


            return query.Include(r => r.EmployeeComponents.Where(k => k.DateDeleted == null))
                            .ThenInclude(r => r.Component).Where(d => d.DateDeleted == null).ToList();
        }
       

    }
}
