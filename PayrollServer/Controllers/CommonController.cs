using AutoMapper;
using Contracts;
using Entities;
using Entities.Enumerations;
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
    public class CommonController : ControllerBase
    {
        private RepositoryContext _repositoryContext;
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        //private ICommonRepository _commonRepository;

        private readonly IMapper _mapper;


        public CommonController(RepositoryContext repositoryContext, ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
             _repositoryContext = repositoryContext;
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
        [Route("currencies")]
        public IEnumerable<Currency> GetAllCurrencies()
        {
            var currencies = _repositoryContext.Currencies.ToList();

            return currencies;
        }
        //[HttpGet]
        //[Route("PaymentDaysTypes")]
        //public IEnumerable<PaymentDaysType> GetAllPaymentDaysType()
        //{
        //    var paymentDaysTypes = _repository.Common.GetPaymentDaysTypes();

        //    return paymentDaysTypes;
        //}

        [HttpGet]
        [Route("EmployeeTypes")]
        public IEnumerable<EmployeeType> GetEmployeeTypes()
        {
            var employeeTypes = _repository.Common.GetEmployeeTypes();

            return employeeTypes;
        }

        [HttpGet]
        [Route("EmployeeGraceTypes")]
        public IEnumerable<EmployeeGraceType> GetEmployeeGraceTypes()
        {
            var types = _repository.Common.GetEmployeeGraceTypes();

            return types;
        }

        [HttpGet]
        [Route("UserClaimTypes")]
        public List<UserClaimDTO> GetUserClaimTypes()
        {
            List< UserClaimDTO> types =
                    new List<UserClaimDTO>();

           
            types.Add(new UserClaimDTO
            {
                ID = UserClaimTypeEnum.Admin,
                Name = "Admin"
            });
            types.Add(new UserClaimDTO
            {
                ID = UserClaimTypeEnum.Analyst,
                Name = "Analyst"
            });
            types.Add(new UserClaimDTO
            {
                ID = UserClaimTypeEnum.Operator,
                Name = "Operator"
            });

            return types;
        }

    }
}
