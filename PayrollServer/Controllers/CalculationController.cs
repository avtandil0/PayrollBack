using AutoMapper;
using Contracts;
using Entities.FilterModels;
using Entities.HelperModels;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

        [HttpPost]
        [Route("paid")]
        public Result CreateEmployee([FromBody] PaidHelper paidDTO)
        {

            _repository.Calculation.CreateCalculation(paidDTO);

            _logger.LogInfo($"Created new Employee.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        [HttpGet]
        [Route("generateExcel")]
        public FileStreamResult GenerateExcel([FromQuery] CalculationFilter calculationFilter)
        {

            //Workbook workbook = new Workbook();

            //Worksheet worksheet = new Worksheet("First Sheet");
            //worksheet.Cells[0, 1] = new Cell((short)1);
            //worksheet.Cells[2, 0] = new Cell(9999999);
            //worksheet.Cells[3, 3] = new Cell((decimal)3.45);
            //worksheet.Cells[2, 2] = new Cell("Text string");
            //worksheet.Cells[2, 4] = new Cell("Second string");
            //worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
            //worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD"); worksheet.Cells.ColumnWidth[0, 1] = 3000;
            //workbook.Worksheets.Add(worksheet);



            ////_logger.LogInfo($"Created new Employee.");

            //var stream = new MemoryStream();
            //workbook.SaveToStream(stream);

            //var mimeType = "Application/vnm.ms-excel";
            //var fileStreamResult = new FileStreamResult(stream, mimeType);
            ////fileStreamResult.FileDownloadName = string.Format("To Bank BOG {0:yyyy_MM_dd_HH_mm_ss}.xlsx", DateTime.Now);

            //stream.Seek(0, SeekOrigin.Begin);


            //return fileStreamResult;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();

            // name of the sheet
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            // setting the properties
            // of the work sheet 
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            // Setting the properties
            //// of the first row
            //workSheet.Row(1).Height = 20;
            ////workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //workSheet.Row(1).Style.Font.Bold = true;

            // Header of the Excel sheet
            workSheet.Cells[1, 1].Value = "xelfasi";
            workSheet.Cells[2, 1].Value = "angariSi";
            workSheet.Cells[2, 2].Value = " biki";
            workSheet.Cells[2, 3].Value = "dasaxeleba";
            workSheet.Cells[2, 4].Value = "daniSnuleba";
            workSheet.Cells[2, 5].Value = "tanxa";

            // Inserting the article data into excel
            // sheet by using the for each loop
            // As we have values to the first row 
            // we will start with second row
            int recordIndex = 3;

            var employees = _repository.Employee.GetCalculationByFilter(calculationFilter);

            foreach (var employee in employees)
            {
                workSheet.Cells[recordIndex, 1].Value = employee.BankAccountNumber;
                workSheet.Cells[recordIndex, 2].Value = "TBCBGE22"; // biki
                workSheet.Cells[recordIndex, 3].Value = String.Format("{0} {1}", employee.FirstName, employee.LastName);
                workSheet.Cells[recordIndex, 4].Value = "xelfasi"; //  daniSnuleba
                workSheet.Cells[recordIndex, 5].Value = employee.Calculations.Sum(r => r.Net);
                recordIndex++;
            }

            // By default, the column width is not 
            // set to auto fit for the content
            // of the range, so we are using
            // AutoFit() method here. 
            // workSheet.Column(1).AutoFit();
            // workSheet.Column(2).AutoFit();
            // workSheet.Column(3).AutoFit();


            var stream = new MemoryStream(excel.GetAsByteArray());

            var mimeType = "Application/vnm.ms-excel";
            var fileStreamResult = new FileStreamResult(stream, mimeType);
            fileStreamResult.FileDownloadName = string.Format("To Bank BOG {0:yyyy_MM_dd_HH_mm_ss}.xlsx", DateTime.Now);

            stream.Seek(0, SeekOrigin.Begin);


            return fileStreamResult;

        }

    }
}
