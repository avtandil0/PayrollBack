using AutoMapper;
using Contracts;
using Entities.FilterModels;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollServer.Models;
using PayrollServer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet]
        [Route("generateExcel")]
        public FileStreamResult GenerateExcel([FromQuery] CalculationFilter calculationFilter)
        {

            Workbook workbook = new Workbook();

            Worksheet worksheet = new Worksheet("First Sheet");
            worksheet.Cells[0, 1] = new Cell((short)1);
            worksheet.Cells[2, 0] = new Cell(9999999);
            worksheet.Cells[3, 3] = new Cell((decimal)3.45);
            worksheet.Cells[2, 2] = new Cell("Text string");
            worksheet.Cells[2, 4] = new Cell("Second string");
            worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
            worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD"); worksheet.Cells.ColumnWidth[0, 1] = 3000;
            workbook.Worksheets.Add(worksheet);



            //_logger.LogInfo($"Created new Employee.");

            var stream = new MemoryStream();
            workbook.SaveToStream(stream);

            var mimeType = "Application/vnm.ms-excel";
            var fileStreamResult = new FileStreamResult(stream, mimeType);
            //fileStreamResult.FileDownloadName = string.Format("To Bank BOG {0:yyyy_MM_dd_HH_mm_ss}.xlsx", DateTime.Now);

            stream.Seek(0, SeekOrigin.Begin);


            return fileStreamResult;

        }

    }
}
