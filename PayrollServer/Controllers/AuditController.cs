using AutoMapper;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollServer.Models;
using PayrollServer.Models.DTOs;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private SynergyContext _repositoryContext;
        private RepositoryContext _payrollContext;
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private ISynergyRepository _synergyRepository;
        private IRepositoryWrapper _repositoryWrapper;


        private readonly IMapper _mapper;


        public AuditController(IRepositoryWrapper repositoryWrapper, SynergyContext repositoryContext, ISynergyRepository sRepository, RepositoryContext payrollContext, ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _repositoryContext = repositoryContext;
            _payrollContext = payrollContext;
            _synergyRepository = sRepository;
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAllHumresAudit")]
        public IEnumerable<HumresAudit> GetAllHumresAudit()
        {
            var humresAudits = _repositoryContext.HumresAudits.OrderByDescending(r => r.DateCreated).ToList();
            return humresAudits;
        }

        [HttpGet]
        [Route("getAllAbsencesAudit")]
        public IEnumerable<AbsencesAudit> getAllAbsencesAudit()
        {
            var humresAudits = _repositoryContext.AbsencesAudits.OrderByDescending(r => r.DateCreated).ToList();
            return humresAudits;
        }



        [HttpPut]
        [Route("updateEmployee")]
        public Result UpdateEmployee(HumresAudit humresAudit)
        {
            var emp = _payrollContext.Employees.FirstOrDefault(r => r.ResId == humresAudit.ResId);

            if (emp == null)
            {
                return new Result(false, 1, "This Employee does not exist in Payroll");
            }

            if (humresAudit.FieldName == "sur_name")
            {
                emp.LastName = humresAudit.NewValue;
            }

            if (humresAudit.FieldName == "adres1")
            {
                emp.Address = humresAudit.NewValue;
            }

            if (humresAudit.FieldName == "ldatindienst")
            {
                emp.ContractStartDate = DateTime.Parse(humresAudit.NewValue);
            }

            if (humresAudit.FieldName == "ldatuitdienst")
            {
                emp.ContractEndDate = DateTime.Parse(humresAudit.NewValue);
            }

            if (humresAudit.FieldName == "NewRecord")
            {
                var list = new List<int>();
                list.Add(humresAudit.ResId);
                var humresFroSynergy = _synergyRepository.GetHumresByIds(list);


                _repositoryWrapper.Employee.ImportEmployee(humresFroSynergy.ToList());
            }


            _payrollContext.SaveChanges();
            return new Result(true, 1, "წარმატებით დასრულდა");

        }



        [HttpPut]
        [Route("updateComponent")]
        public Result UpdateComponent(AbsencesAudit absencesAudit)
        {
            var component = _payrollContext.EmployeeComponents.FirstOrDefault(r => r.Hid == absencesAudit.Hid);

            if (component == null)
            {
                return new Result(false, 1, "This Component does not exist in Payroll");
            }

            var currentDate = DateTime.Now;

            if (absencesAudit.FieldName == "StartDate")
            {
                component.StartDate = DateTime.Parse(absencesAudit.NewValue);
            }

            if (absencesAudit.FieldName == "EndDate")
            {
                component.EndDate = DateTime.Parse(absencesAudit.NewValue);
            }

            if (absencesAudit.FieldName == "Amount")
            {
                component.EndDate = currentDate.AddDays(-1);
                var EmpComp = new EmployeeComponent
                {
                    Id = Guid.NewGuid(),
                    Hid = component.Hid,
                    EndDate = null,
                    Amount = decimal.Parse(absencesAudit.NewValue),
                    CashAmount = component.CashAmount,
                    ComponentId = component.ComponentId,
                    Currency = component.Currency,
                    DateCreated = currentDate,
                    EmployeeId = component.EmployeeId,
                    IsPermanent = component.IsPermanent,
                    PaidByCash = component.PaidByCash,
                    PaidMultiple = component.PaidMultiple,
                    PaymentDaysType = component.PaymentDaysType,
                    PaymentDaysTypeId = component.PaymentDaysTypeId,
                    SchemeTypeId = component.SchemeTypeId,
                    CostCenterId = component.CostCenterId,
                    ProjectId = component.ProjectId,
                    StartDate = currentDate

                };

                _payrollContext.EmployeeComponents.Add(EmpComp);
            }






            _payrollContext.SaveChanges();
            return new Result(true, 1, "წარმატებით დასრულდა");

        }



    }
}
