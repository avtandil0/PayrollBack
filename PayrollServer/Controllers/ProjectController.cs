﻿using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

            IEnumerable<ProjectDTO> projectDTOs = _mapper.Map<IEnumerable<ProjectDTO>>(departmenrts);

            _logger.LogInfo($"Returned all Projects from database.");

            return projectDTOs;
        }

        [HttpPost]
        public Result CreateDepartment([FromBody] ProjectDTO projectDTO)
        {
            Project project = _mapper.Map<Project>(projectDTO);

            _repository.Project.CreateProject(project);

            _logger.LogInfo($"Created new Cepartment.");

            return new Result(true, 1, "წარმატებით დასრულდა");
        }

        [HttpPut]
        public Result UpdateDepartment([FromBody] ProjectDTO projectDTO)
        {
            Project project = _mapper.Map<Project>(projectDTO);

            _repository.Project.UpdateProject(project);

            _logger.LogInfo($"Update Project id = &{project.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");
        }

        [HttpDelete]
        public Result DeleteDepartment([FromBody] ProjectDTO projectDTO)
        {
            Project project = _mapper.Map<Project>(projectDTO);

            _repository.Project.DeleteProject(project);

            _logger.LogInfo($"Delete Project id = &{project.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");
        }
    }
}
