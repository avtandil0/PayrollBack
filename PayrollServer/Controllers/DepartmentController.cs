using PayrollServer.Models;
using PayrollServer.Models.DTOs;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        private RepositoryContext _repositoryContext;



        public DepartmentController(ILoggerManager logger, IRepositoryWrapper repository, 
            IMapper mapper, RepositoryContext repositoryContext)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _repositoryContext = repositoryContext;
        }

        public class DepChildren
        {
            public Guid Id { get; set; }
            public string  FullName { get; set; }
        }
        public class DepResult
        {
            public Guid DepId { get; set; }
            public string DepName { get; set; }
            public List<DepChildren> Children { get; set; }
        }

        [HttpGet]
        [Route("GetAllDepartmentAndEmployees")]
        public IEnumerable<DepResult> GetAllDepartmentAndEmployees()
        {
            var result = _repositoryContext.Departments.Where(r => r.DateDeleted == null)
                                    .Include(u => u.Employees.Where(e => e.DateDeleted == null)
                                     );
            var result1 = result.Select(r => new DepResult
            {
                DepId = r.Id,
                DepName = r.Name,
                Children = r.Employees.Where(e => e.DateDeleted == null).Select(e => new DepChildren
                {
                    Id = e.Id,
                    FullName = e.FirstName + " " + e.LastName
                }).ToList()
            }).ToList();
            return result1;
        }

        [HttpGet]
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departmenrts = _repository.Department.GetAllDepartments();

            IEnumerable<DepartmentDTO> departmentDTOs = _mapper.Map<IEnumerable<DepartmentDTO>>(departmenrts);

            _logger.LogInfo($"Returned all departmenrts from database.");

            return departmentDTOs;
        }

       

        [HttpPost]
        public Result CreateDepartment([FromBody] DepartmentDTO departmentDTO)
        {
            Department department = _mapper.Map<Department>(departmentDTO);

            _repository.Department.CreateDepartment(department);

            _logger.LogInfo($"Created new Department.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpPut]
        public Result UpdateDepartment([FromBody] DepartmentDTO departmentDTO)
        {
            Department department = _mapper.Map<Department>(departmentDTO);

            _repository.Department.UpdateDepartment(department);

            _logger.LogInfo($"Update Department id = &{department.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpDelete]
        public Result DeleteDepartment([FromBody] DepartmentDTO departmentDTO)
        {
            Department department = _mapper.Map<Department>(departmentDTO);

            _repository.Department.DeleteDepartment(department);

            _logger.LogInfo($"Delete Department id = &{department.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }



    }
}
