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
    public class CoefficientController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CoefficientController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CoefficientDTO> GetAllCoefficients()
        {
            var coefficients = _repository.Coefficient.GetAllCoefficients();

            IEnumerable<CoefficientDTO> coefficientDTOs = _mapper.Map<IEnumerable<CoefficientDTO>>(coefficients);

            _logger.LogInfo($"Returned all Coefficients from database.");

            return coefficientDTOs;
        }

        [HttpPost]
        public Result CreateCoefficient([FromBody] CoefficientDTO coefficientDTO)
        {
            Coefficient coefficient = _mapper.Map<Coefficient>(coefficientDTO);

            _repository.Coefficient.CreateCoefficient(coefficient);

            _logger.LogInfo($"Created new Coefficient.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

    }
}
