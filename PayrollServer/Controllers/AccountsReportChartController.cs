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
    public class AccountsReportChartController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public AccountsReportChartController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<AccountsReportChartDTO> GetAllCostCenter()
        {
            var accountsReportCharts = _repository.AccountsReportChart.GetAllAccountsReportChart();

            IEnumerable<AccountsReportChartDTO> costCenterDTOs = _mapper.Map<IEnumerable<AccountsReportChartDTO>>(accountsReportCharts);

            _logger.LogInfo($"Returned all AccountsReportChart from database.");

            return costCenterDTOs;
        }

        [HttpPost]
        public Result CreateDepartment([FromBody] AccountsReportChartDTO accountsReportChartDTO)
        {
            AccountsReportChart accountsReportChart = _mapper.Map<AccountsReportChart>(accountsReportChartDTO);

            _repository.AccountsReportChart.CreateAccountsReportChart(accountsReportChart);

            _logger.LogInfo($"Created new AccountsReportChart.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

    }
}
