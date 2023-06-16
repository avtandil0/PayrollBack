using AutoMapper;
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
    public class CostCenterController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CostCenterController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CostCenterDTO> GetAllCostCenter()
        {
            var costCenters = _repository.CostCenter.GetAllCostCenter();

            IEnumerable<CostCenterDTO> costCenterDTOs = _mapper.Map<IEnumerable<CostCenterDTO>>(costCenters);

            _logger.LogInfo($"Returned all CostCenter from database.");

            return costCenterDTOs;
        }

        [HttpPost]
        public Result CreateCostCenter([FromBody] CostCenterDTO costCenterDTO)
        {
            CostCenter costCenter = _mapper.Map<CostCenter>(costCenterDTO);

            _repository.CostCenter.CreateCostCenter(costCenter);

            _logger.LogInfo($"Created new CostCenter.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpPut]
        public Result UpdateCostCenter([FromBody] CostCenterDTO costCenterDTO)
        {
            CostCenter costCenter = _mapper.Map<CostCenter>(costCenterDTO);

            _repository.CostCenter.UpdateCostCenter(costCenter);

            _logger.LogInfo($"Update CostCenter id = &{costCenter.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpDelete]
        public Result DeleteCostCenter([FromBody] CostCenterDTO costCenterDTO)
        {
            CostCenter costCenter = _mapper.Map<CostCenter>(costCenterDTO);

            _repository.CostCenter.DeleteCostCenter(costCenter);

            _logger.LogInfo($"Delete CostCenter id = &{costCenter.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }
    }
}
