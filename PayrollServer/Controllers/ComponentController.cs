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
            var components = _repository.Component.GetAllComponents();

            IEnumerable<ComponentDTO> componentDTOs = _mapper.Map<IEnumerable<ComponentDTO>>(components);
            
            var current = DateTime.Now;
            foreach (var comp in componentDTOs)
            {
                comp.TypeName = comp.Type == 1 ? "დარიცხვა" : "დაკავება";
                comp.Status = new ObjectStatus();
                if (comp.StartDate.Date <= current.Date && comp.EndDate.Date >= current.Date)
                {
                    comp.Status.Value = 1;
                }
                else
                {
                    comp.Status.Value = 0;
                    comp.Status.FieldNames = new List<string>();
                    if (comp.StartDate.Date > current.Date)
                    {
                        comp.Status.FieldNames.Add("StartDate");
                    }
                    if (comp.EndDate.Date < current.Date)
                    {
                        comp.Status.FieldNames.Add("EndDate");
                    }
                }
            }
            
            _logger.LogInfo($"Returned all departmenrts from database.");

            return componentDTOs;
        }


        [HttpGet]
        [Route("getAllActive")]
        public IEnumerable<ComponentDTO> GetAllActiveComponents()
        {
            var components = _repository.Component.GetAllActiveComponents();

            IEnumerable<ComponentDTO> componentDTOs = _mapper.Map<IEnumerable<ComponentDTO>>(components);
            
            _logger.LogInfo($"Returned all departmenrts from database.");

            return componentDTOs;
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
