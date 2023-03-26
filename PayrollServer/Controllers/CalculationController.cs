using AutoMapper;
using Contracts;
using Entities;
using Entities.Enumerations;
using Entities.FilterModels;
using Entities.HelperModels;
using Entities.Models;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PayrollServer.Models;
using PayrollServer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private RepositoryContext _repositoryContext;
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private ISynergyRepository _sunergyRepository;
        public CalculationController(RepositoryContext repositoryContext, ILoggerManager logger, IRepositoryWrapper repository, ISynergyRepository synergyRepository, IMapper mapper)
        {
            _repositoryContext = repositoryContext;
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

        [HttpGet]
        [Route("GetDeclaration")]
        public IEnumerable<PayrollReportDatum> GetDeclaration()
        {
            var declarations = _repositoryContext.PayrollReportData.ToList();

            return declarations;
        }

        [HttpPost]
        [Route("calculateForDeclaration")]
        public Result calculateForDeclaration()
        {
            string query = @"produce_payroll_report_data";
            string connectionString = "Server=AZENAISHVILI1;database=PayrollNew;Trusted_Connection=True;User ID=PayrollModule;Password=NewPass1;";
            try
            {
                var rowsAffected = _repositoryContext.Database.ExecuteSqlRaw(query);


                return new Result(true, 1, rowsAffected.ToString() + " row(s) affected");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);

            }

            return new Result(false, 0, "Errorr ! ! !");

        }

        [HttpPost]
        [Route("calculateForDeclaration1")]
        public Result calculateForDeclaration1()
        {
            string query = @"produce_payroll_report_data";
            string connectionString = "Server=AZENAISHVILI1;database=PayrollNew;Trusted_Connection=True;User ID=PayrollModule;Password=NewPass1;";
            try
            {
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.StoredProcedure;


                // Execute the command.

                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();



                // Display the result of the operation.


                return new Result(true, 1, rowsAffected.ToString() + " row(s) affected");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);

            }

            return new Result(false, 0, "Errorr ! ! !");

        }

        [HttpPost]
        [Route("calculate/{calculationDate}")]
        public Result CreateEmployee([FromBody] CalculationFilter calculationFilter, DateTime calculationDate)
        {

            try
            {
                _repository.Calculation.CreateCalculation(calculationFilter, calculationDate);

            }
            catch (Exception e)
            {
                return new Result(false, 0, e.Message);
            }


            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        class FileEmployee
        {
            public string PersonalNumber { get; set; }
            public Decimal Amount { get; set; }
        }

        [HttpPost]
        [Route("CreateEmployeeFromFile")]
        public async Task<Result> CreateEmployeeFromFileAsync([FromForm] IFormFile file)
        {

            //var rootFolder = @"E:\Files";//@"D:\Files";

            //var fileName = file.FileName;
            //var filePath = Path.Combine(rootFolder, fileName);
            //var fileLocation = new FileInfo(filePath);

            //using (var fileStream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(fileStream);
            //}

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Summary"];
                //var rootFolder = appConfig.Value.FileLocationConfig.Location;//@"D:\Files";

                int totalRows = workSheet.Dimension.Rows;
                var rowLength = workSheet.Dimension.End.Row;


                List<FileEmployee> employees = new List<FileEmployee>();

                var componentName = workSheet.Cells[3, 9].Value.ToString();

                for (int i = 2; i <= rowLength; i++)
                {
                    FileEmployee fileEmployee = new FileEmployee
                    {
                        Amount = Decimal.Parse(workSheet.Cells[i, 8].Value.ToString()),
                        PersonalNumber = workSheet.Cells[i, 7].Value.ToString()
                    };

                    employees.Add(fileEmployee);
                }


                var personalNumbers = employees.Select(x => x.PersonalNumber.Trim()).Distinct().ToList();
                var empFromDb = _repositoryContext.Employees
                            .Where(r => personalNumbers.Contains(r.PersonalNumber))
                            .ToList();

                var grossComponent = _repositoryContext.Components.First(r => r.Name.Trim() == componentName.Trim());

                //var empComp = _repositoryContext.EmployeeComponents.
                var currentDate = DateTime.Now;

                List<Calculation> calculations = new List<Calculation>();

                foreach (var item in employees)
                {
                    var emp = empFromDb.FirstOrDefault(r => r.PersonalNumber == item.PersonalNumber);
                    if (emp != null)
                    {
                        var calculation = GetCalculationObjectForFile(currentDate, grossComponent, emp, item.Amount);
                        calculations.Add(calculation);
                    }

                }
                _repositoryContext.AddRange(calculations);
                _repositoryContext.SaveChanges();


            }

            //var rate = _repositoryContext.Rates.FirstOrDefault(r => r.Date == calculationDate);

            //try
            //{
            //    _repository.Calculation.CreateCalculation(calculationFilter, calculationDate);

            //}
            //catch (Exception e)
            //{
            //    return new Result(false, 0, e.Message);
            //}


            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        private Calculation GetCalculationObjectForFile(DateTime calculationDate, Component component, Employee employee, decimal amount)
        {
            var coefficient = _repositoryContext.Coefficients.Where(r => r.Id == component.CoefficientId && r.DateDeleted == null).FirstOrDefault();

            Calculation calculation = new Calculation();



            var empCompAmount = amount;

            empCompAmount = Math.Round(empCompAmount, 2);

            calculation.Id = Guid.NewGuid();
            calculation.CalculationDate = calculationDate;
            calculation.DateCreated = DateTime.Now;

            calculation.EmployeeId = employee.Id;
            //calculation.EmployeeComponentId = empComp.Id;


            calculation.ResId = employee.ResId;
            calculation.CompCode = component.Name;

            calculation.PayrollYear = calculationDate.Year;
            calculation.PayrollMonth = calculationDate.Month;
            //calculation.SchemeTypeId = empComp.SchemeTypeId;
            calculation.RemainingGraceAmount = 0;



            calculation.Currency = "GEL";

            //empComp.Amount = (decimal)(empComp.Amount * rate.ExchangeRate);

            if (employee.SchemeTypeId == (int)SchemeTypeEnum.Standart)
            {
                calculation.Gross = empCompAmount * (decimal)coefficient.Sgross;
                calculation.Net = empCompAmount * (decimal)coefficient.Snet;
                calculation.Paid = empCompAmount * (decimal)coefficient.Spaid;
                calculation.PensionTax = empCompAmount * (decimal)coefficient.Spension;
                calculation.IncomeTax = empCompAmount * (decimal)coefficient.SincomeTax;
            }

            if (employee.SchemeTypeId == (int)SchemeTypeEnum.Pension)
            {
                calculation.Gross = empCompAmount * (decimal)coefficient.Pgross;
                calculation.Net = empCompAmount * (decimal)coefficient.Pnet;
                calculation.Paid = empCompAmount * (decimal)coefficient.Ppaid;
                calculation.PensionTax = empCompAmount * (decimal)coefficient.Ppension;
                calculation.IncomeTax = empCompAmount * (decimal)coefficient.PincomeTax;
            }

            if (employee.RemainingGraceAmount > 0)
            {
                var rem = employee.RemainingGraceAmount - (calculation.Gross - calculation.PensionTax);
                if (rem > 0)
                {
                    employee.RemainingGraceAmount = rem;
                    calculation.RemainingGraceAmount = rem;
                    calculation.IncomeTax = 0;
                    calculation.Net = calculation.Gross - calculation.PensionTax;
                }
                else
                {
                    calculation.IncomeTax = (decimal)((calculation.Gross - calculation.PensionTax - employee.RemainingGraceAmount) / 5);
                    calculation.Net = calculation.Gross - calculation.PensionTax - calculation.IncomeTax;
                    employee.RemainingGraceAmount = 0;
                    calculation.RemainingGraceAmount = 0;
                }

            }

            return calculation;
        }

        //public DateTime? CalculationPeriod { get; set; }
        public class DeleteParams
        {
            public Guid Id { get; set; }
            public DateTime? CalculationPeriod { get; set; }
        }
        [HttpDelete]
        [Route("deleteCalculations")]
        public Result DeleteCalculations([FromQuery] DeleteParams deleteParams)
        {
            var items = _repositoryContext.Calculations.Where(r => r.EmployeeId == deleteParams.Id);
            if (deleteParams.CalculationPeriod != null)
            {
                items = items.Where(r => r.PayrollYear == deleteParams.CalculationPeriod.Value.Year &&
                                r.PayrollMonth == deleteParams.CalculationPeriod.Value.Month);
            }

            var employee = _repositoryContext.Employees.Where(r => r.Id == deleteParams.Id).FirstOrDefault();
            if (employee != null)
            {
                employee.RemainingGraceAmount = employee.GraceAmount;
            }

            _repositoryContext.Employees.Update(employee);
            _repositoryContext.Calculations.RemoveRange(items);
            _repositoryContext.SaveChanges();
            _logger.LogInfo($"Calculation deleted");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }


        [HttpDelete]
        [Route("delete/{id}")]
        public Result DeleteCalculation(Guid id)
        {

            var item = _repositoryContext.Calculations.Where(r => r.Id == id).FirstOrDefault();
            //item.DateDeleted = DateTime.Now;

            var nextCalculations = _repositoryContext.Calculations.Where(r => r.PayrollYear == item.PayrollYear
                                && r.CalculationDate > item.CalculationDate)
                                .OrderBy(r=>r.CalculationDate);
            var employee = _repositoryContext.Employees.Where(r => r.Id == item.EmployeeId).FirstOrDefault();

            var lastCalc = _repositoryContext.Calculations.Where(r=>r.EmployeeId == item.EmployeeId && r.PayrollYear == item.PayrollYear
                        && r.CalculationDate < item.CalculationDate)
                        .OrderByDescending(r => r.CalculationDate)//.ThenBy(r => r.DateCreated)
                                   .FirstOrDefault();

            var correctRemaining = employee.GraceAmount;
            if(lastCalc == null)
            {
                correctRemaining = employee.GraceAmount;
            }
            else
            {
                correctRemaining = lastCalc.RemainingGraceAmount;
            }
            if(employee == null)
            {
                return new Result(false, 0, "Employee Null");
            }

            var remaining = correctRemaining - (item.Gross - item.PensionTax);
            if (!nextCalculations.Any())
            {
                employee.RemainingGraceAmount = correctRemaining;
            }

            foreach (var calc in nextCalculations)
            {
                //employee.RemainingGraceAmount = remaining;
                //calc.RemainingGraceAmount = remaining;
                if(correctRemaining > 0)
                {
                    calc.IncomeTax = 0;
                    calc.Net = calc.Gross - calc.PensionTax;
                    calc.RemainingGraceAmount = correctRemaining - (calc.Gross - calc.PensionTax);
                    employee.RemainingGraceAmount = correctRemaining - (calc.Gross - calc.PensionTax); ;
                }
                correctRemaining = correctRemaining - (calc.Gross - calc.PensionTax);
            }

            _repositoryContext.Calculations.Remove(item);
            _repositoryContext.Employees.Update(employee);
            _repositoryContext.Calculations.UpdateRange(nextCalculations);

            _repositoryContext.SaveChanges();
            _logger.LogInfo($"Calculation deleted");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }


        [HttpPost]
        [Route("paid")]
        public Result CreateEmployee([FromBody] PaidHelper paidDTO)
        {

            _repository.Calculation.Paid(paidDTO);

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
