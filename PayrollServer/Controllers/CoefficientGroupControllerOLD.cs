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
    public class CoefficientGroupControllerOLD : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CoefficientGroupControllerOLD(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //[HttpGet]
        //public IEnumerable<CoefficientGroupDTO> GetAllCoefficientGroup()
        //{
        //    var coefficientGroups = _repository.CoefficientGroup.GetAllCoefficientGroup();

        //    IEnumerable<CoefficientGroupDTO> coefficientGroupDTOs = _mapper.Map<IEnumerable<CoefficientGroupDTO>>(coefficientGroups);

        //    _logger.LogInfo($"Returned all CostCenter from database.");

        //    return coefficientGroupDTOs;
        //}

        //[HttpPost]
        //public Result CreateDepartment([FromBody] CoefficientGroupDTO coefficientGroupDTO)
        //{
        //    CoefficientGroup coefficientGroup = _mapper.Map<CoefficientGroup>(coefficientGroupDTO);

        //    _repository.CoefficientGroup.CreateCoefficientGroup(coefficientGroup);

        //    _logger.LogInfo($"Created new CostCenter.");

        //    return new Result(true, 1, "წარმატებით დასრულდა");

        //}

    }
}
