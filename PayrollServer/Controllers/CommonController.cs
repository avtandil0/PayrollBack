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
    public class CommonController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        //private ICommonRepository _commonRepository;

        private readonly IMapper _mapper;


        public CommonController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("accountsReportChartTypes")]
        public IEnumerable<AccountsReportChartTypeDTO> GetAllAccountsReportChartTypes()
        {
            var costCenters = _repository.Common.GetAccountsReportChartTypes();

            IEnumerable<AccountsReportChartTypeDTO> accountsReportChartTypeDTOs =
                _mapper.Map<IEnumerable<AccountsReportChartTypeDTO>>(costCenters);

            //_logger.LogInfo($"Returned all accountsReportChartTypeDTOs from database.");

            return accountsReportChartTypeDTOs;
        }

        [HttpGet]
        [Route("schemeTypes")]
        public IEnumerable<SchemeType> GetAllSchemeTypes()
        {
            var schemeTypes = _repository.Common.GetSchemeTypes();

            return schemeTypes;
        }

        [HttpGet]
        [Route("PaymentDaysTypes")]
        public IEnumerable<PaymentDaysType> GetAllPaymentDaysType()
        {
            var paymentDaysTypes = _repository.Common.GetPaymentDaysTypes();

            return paymentDaysTypes;
        }


    }
}
