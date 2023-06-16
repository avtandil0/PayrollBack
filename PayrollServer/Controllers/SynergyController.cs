using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollServer.Models.DTOs;
using PayrollServer.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SynergyController : ControllerBase
    {
        private ILoggerManager _logger;
        private ISynergyRepository _repository;
        private readonly IMapper _mapper;


        public SynergyController(ILoggerManager logger, ISynergyRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAllHumres")]
        public IEnumerable<HumreDTO> GetAllHumres()
        {
            var humres = _repository.GetHumres();

            IEnumerable<HumreDTO> humreeDTOs =
                _mapper.Map<IEnumerable<HumreDTO>>(humres);

            //_logger.LogInfo($"Returned all accountsReportChartTypeDTOs from database.");

            return humreeDTOs;
        }

        [HttpGet]
        [Route("humres")]
        public IEnumerable<HumreDTO> GetHumresByFilter([FromQuery]HumreFilter humre)
        {
            var humres = _repository.GetHumresByFilter(humre);

            IEnumerable<HumreDTO> humreeDTOs =
                _mapper.Map<IEnumerable<HumreDTO>>(humres);

            //_logger.LogInfo($"Returned all accountsReportChartTypeDTOs from database.");

            return humreeDTOs;
        }
    }
}
