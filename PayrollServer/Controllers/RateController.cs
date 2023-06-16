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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private RepositoryContext _repositoryContext;
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public RateController(RepositoryContext repositoryContext, ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _repositoryContext = repositoryContext;
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Rate> GetAllRates()
        {
            var rates = _repositoryContext.Rates.Include(r => r.Currency).ToList();


            return rates;
        }

        [HttpPost]
        public Result CreateRate([FromBody] Rate rate)
        {
            rate.Id = Guid.NewGuid();
            _repositoryContext.Rates.Add(rate);

            _repositoryContext.SaveChanges();


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
        public Result DeleteDepartment([FromBody] Rate rate)
        {

            _repositoryContext.Rates.Remove(rate);

            _repositoryContext.SaveChanges();
            return new Result(true, 1, "წარმატებით დასრულდა");
        }
    }
}
