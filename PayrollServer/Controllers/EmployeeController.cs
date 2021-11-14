﻿using AutoMapper;
using Contracts;
using Entities.FilterModels;
using Entities.HelperModels;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using PayrollServer.Models;
using PayrollServer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private ISynergyRepository _sunergyRepository;
        public EmployeeController(ILoggerManager logger, IRepositoryWrapper repository, ISynergyRepository synergyRepository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _sunergyRepository = synergyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getEmployee/{id}")]
        public EmployeeDTO GetAllEmployees(Guid id)
        {
            var employees = _repository.Employee.GetEmployeeById(id);

            EmployeeDTO employeeDTOs = _mapper.Map<EmployeeDTO>(employees);

            _logger.LogInfo($"Returned all Employees from database.");

            return employeeDTOs;
        }

        [HttpGet]
        [Route("GetEmployeeByCalculationFilter")]
        public IEnumerable<Employee> GetEmployeeByCalculationFilter([FromQuery] CalculationFilter calculationFilter)
        {
            var employees = _repository.Employee.GetCalculationByFilter(calculationFilter);

            //IEnumerable<EmployeeCalculationDTO> calculationDTOs = _mapper.Map<IEnumerable<EmployeeCalculationDTO>>(employees);

            return employees;
        }

        [HttpGet]
        [Route("getEmployeeByDepartment/{id}")]
        public IEnumerable<EmployeeDTO> GetEmployeeByDepartment(Guid id)
        {
            var employees = _repository.Employee.GetEmployeeByDepartment(id);

            IEnumerable<EmployeeDTO> employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            _logger.LogInfo($"Returned all departmenrts from database.");

            return employeeDTOs;
        }

        [HttpGet]
        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var employees = _repository.Employee.GetAllEmployees();

            IEnumerable<EmployeeDTO> employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            _logger.LogInfo($"Returned all Employees from database.");

            return employeeDTOs;
        }

        [HttpPost]
        public Result CreateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeDTO);

            _repository.Employee.CreateEmployee(employee);

            _logger.LogInfo($"Created new Employee.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpPost]
        [Route("importFromSynergy")]
        public Result ImportFromSynergy([FromBody] List<int> humreIds)
        {
            var humresFroSynergy = _sunergyRepository.GetHumresByIds(humreIds);


            _repository.Employee.ImportEmployee(humresFroSynergy.ToList());

            _logger.LogInfo($"Imported new Employee From Synergy");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpPut]
        public Result UpdateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeDTO);

            _repository.Employee.UpdateEmployee(employee);

            _logger.LogInfo($"Update Employee id = &{employee.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpDelete]
        public Result DeleteEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeDTO);

            _repository.Employee.DeleteEmployee(employee);

            _logger.LogInfo($"Delete Employee id = &{employee.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }
    }
}
