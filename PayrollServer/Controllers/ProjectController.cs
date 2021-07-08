using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
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
    public class ProjectController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public ProjectController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ProjectDTO> GetAllDepartments()
        {
            var departmenrts = _repository.Project.GetAllProjects();

            IEnumerable<ProjectDTO> departmentDTOs = _mapper.Map<IEnumerable<ProjectDTO>>(departmenrts);

            _logger.LogInfo($"Returned all Projects from database.");

            return departmentDTOs;
        }

        [HttpPost]
        public Result CreateDepartment([FromBody] ProjectDTO projectDTO)
        {
            Project project = _mapper.Map<Project>(projectDTO);

            _repository.Project.CreateProject(project);

            _logger.LogInfo($"Created new Cepartment.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }
    }
}
