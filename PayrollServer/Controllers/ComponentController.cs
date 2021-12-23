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
    public class ComponentController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public ComponentController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ComponentDTO> GetAllComponents()
        {
            var departmenrts = _repository.Component.GetAllComponents();

            IEnumerable<ComponentDTO> departmentDTOs = _mapper.Map<IEnumerable<ComponentDTO>>(departmenrts);

            _logger.LogInfo($"Returned all departmenrts from database.");

            return departmentDTOs;
        }


        [HttpGet]
        [Route("getAllActive")]
        public IEnumerable<ComponentDTO> GetAllActiveComponents()
        {
            var departmenrts = _repository.Component.GetAllActiveComponents();

            IEnumerable<ComponentDTO> departmentDTOs = _mapper.Map<IEnumerable<ComponentDTO>>(departmenrts);

            _logger.LogInfo($"Returned all departmenrts from database.");

            return departmentDTOs;
        }

        [HttpPost]
        public Result CreateDepartment([FromBody] ComponentDTO componentDTO)
        {
            Component component = _mapper.Map<Component>(componentDTO);

            _repository.Component.CreateComponent(component);

            _logger.LogInfo($"Created new Component.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpPut]
        public Result UpdateDepartment([FromBody] ComponentDTO componentDTO)
        {
            Component component = _mapper.Map<Component>(componentDTO);

            _repository.Component.UpdateComponent(component);

            _logger.LogInfo($"Update Component id = &{component.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpDelete]
        public Result DeleteDepartment([FromBody] ComponentDTO componentDTO)
        {
            Component component = _mapper.Map<Component>(componentDTO);

            _repository.Component.DeleteComponent(component);

            _logger.LogInfo($"Delete Component id = &{component.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }



    }
}
