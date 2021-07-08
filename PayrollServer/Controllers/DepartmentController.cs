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

namespace PayrollServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public DepartmentController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
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

            _logger.LogInfo($"Created new Cepartment.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }



    }
}
