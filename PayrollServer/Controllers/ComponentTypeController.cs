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

namespace PayrollServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentTypeController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        private RepositoryContext _repositoryContext;



        public ComponentTypeController(ILoggerManager logger, IRepositoryWrapper repository,
            IMapper mapper, RepositoryContext repositoryContext)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryContext = repositoryContext;
        }



        [HttpGet]
        public IEnumerable<ComponentType> GetAllComponentTypes()
        {
            var componentTypes = _repositoryContext.ComponentTypes.Where(r => r.DateDeleted == null);
            return componentTypes;
        }



        [HttpPost]
        public Result CreateDepartment([FromBody] ComponentType componentType)
        {

            componentType.DateCreated = DateTime.Now;
            _repositoryContext.ComponentTypes.Add(componentType);

            _repositoryContext.SaveChanges();
            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpPut]
        public Result UpdateDepartment([FromBody] ComponentType componentType)
        {

            var data = _repositoryContext.ComponentTypes.Where(r => r.DateDeleted == null && componentType.Id == r.Id).FirstOrDefault();

            if (data != null)
            {
                data.Name = componentType.Name;
                data.DateChange = DateTime.Now;
            }

            _repositoryContext.ComponentTypes.Update(data);

            _repositoryContext.SaveChanges();
            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpDelete]
        public Result DeleteDepartment([FromBody] ComponentType componentType)
        {

            _repositoryContext.ComponentTypes.Remove(componentType);
            _repositoryContext.SaveChanges();

            return new Result(true, 1, "წარმატებით დასრულდა");

        }



    }
}
