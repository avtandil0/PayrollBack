using AutoMapper;
using Contracts;
using Entities.FilterModels;
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
    public class CalculationController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private ISynergyRepository _sunergyRepository;
        public CalculationController(ILoggerManager logger, IRepositoryWrapper repository, ISynergyRepository synergyRepository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _sunergyRepository = synergyRepository;
            _mapper = mapper;
        }

        //[HttpGet]
        //[Route("GetCalculationByFilter")]
        //public IEnumerable<CalculationDTO> GetCalculationByFilter([FromQuery]CalculationFilter calculationFilter)
        //{
        //    var calculations = _repository.Calculation.GetCalculationByFilter(calculationFilter);

        //    IEnumerable<CalculationDTO> calculationDTOs = _mapper.Map<IEnumerable<CalculationDTO>>(calculations);

        //    return calculationDTOs;
        //}

        [HttpPost]
        [Route("calculate/{calculationDate}")]
        public Result CreateEmployee([FromBody] CalculationFilter calculationFilter, DateTime calculationDate)
        {

            _repository.Calculation.CreateCalculation(calculationFilter, calculationDate);

            _logger.LogInfo($"Created new Employee.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

    }
}
