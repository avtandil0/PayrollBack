using AutoMapper;
using Contracts;
using Entities;
using Entities.Enumerations;
using Entities.FilterModels;
using Entities.HelperModels;
using Entities.Models;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public IEnumerable<PayrollReportDatum> GetDeclaration(int year, int month)
        {
            var declarations = _repositoryContext.PayrollReportData.Select(r => r);
            if (year != null)
            {
                declarations = declarations.Where(r => r.PayrollYear == year);
            }
            if (month != null)
            {
                declarations = declarations.Where(r => r.Period == month);
            }
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
                //var rowsAffected = _repositoryContext.Database.ExecuteSqlRaw(query);


                //return new Result(true, 1, rowsAffected.ToString() + " row(s) affected");

                var returnValue = new SqlParameter
                {
                    ParameterName = "@ReturnValue",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };
                var numberOfRowsAffected = 0;
                var rowsAffected = _repositoryContext.Database.ExecuteSqlRaw("EXEC produce_payroll_report_data @ReturnValue OUTPUT", returnValue);

                if (returnValue.Value != null)
                {
                    numberOfRowsAffected = (int)returnValue.Value;
                }


                return new Result(true, 1, numberOfRowsAffected.ToString() + " row(s) affected");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);

            }

            return new Result(false, 0, "Errorr ! ! !");

        }

        [HttpPost]
        [Route("deleteAndCalculateForDeclaration")]
        public Result deleteAndCalculateForDeclaration()
        {
            var currentDate = DateTime.Now;
            string deleteQuery = @"delete from payroll_report_data where payrollYear = " + currentDate.Year;
            string query = @" produce_payroll_report_data";
            try
            {
                var returnValue = new SqlParameter
                {
                    ParameterName = "@ReturnValue",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };
                var rowsAffected = _repositoryContext.Database.ExecuteSqlRaw(deleteQuery);
                rowsAffected = _repositoryContext.Database.ExecuteSqlRaw("EXEC produce_payroll_report_data @ReturnValue OUTPUT", returnValue);
                int numberOfRowsAffected = (int)returnValue.Value;


                return new Result(true, 1, numberOfRowsAffected.ToString() + " row(s) affected");
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


        public class AddComponentObject
        {
            public decimal Amount { get; set; }
            public Guid ComponentId { get; set; }
            public int Currency { get; set; }
            public DateTime Date { get; set; }
        }
        public class addCalculationParams
        {
            public List<Guid> EmployeeIds { get; set; }
            public AddComponentObject AddComponentObject { get; set; }
        }
        [HttpPost]
        [Route("addCalculation")]
        public Result AddCalculation([FromBody] addCalculationParams addCalculationParams)
        {

            try
            {
                _repository.Calculation.CreateCalculationWithComponent(addCalculationParams.EmployeeIds,
                        addCalculationParams.AddComponentObject.Date, addCalculationParams.AddComponentObject.ComponentId,
                        addCalculationParams.AddComponentObject.Amount, addCalculationParams.AddComponentObject.Currency);

            }
            catch (Exception e)
            {
                return new Result(false, 0, e.Message);
            }


            return new Result(true, 1, String.Format("კალკულაცია წარმატებით დასრულდა {0} თანამშრომელზე.",
                         addCalculationParams.EmployeeIds.Count()));

        }

        [HttpPost]
        [Route("calculate/{calculationDate}")]
        public Result CreateEmployee([FromBody] List<Guid> employeeIds, DateTime calculationDate)
        {

            try
            {
                _repository.Calculation.CreateCalculation(employeeIds, calculationDate);

            }
            catch (Exception e)
            {
                return new Result(false, 0, e.Message);
            }


            return new Result(true, 1, String.Format("კალკულაცია წარმატებით დასრულდა {0} თანამშრომელზე.",
                         employeeIds.Count()));

        }

        public class FileEmployee
        {
            public string Fullname { get; set; }
            public string ResId { get; set; }
            public string GLAccount { get; set; }
            public string Project { get; set; }
            public string CostCenter { get; set; }
            public string CostUnit { get; set; }
            public string PersonalNumber { get; set; }
            public Decimal Amount { get; set; }
            public string Component { get; set; }
        }

        public class CreateEmployeeFromFileResult
        {
            public List<FileEmployee> Employees { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        [HttpPost]
        [Route("CreateEmployeeFromFile/{date}")]
        public async Task<CreateEmployeeFromFileResult> CreateEmployeeFromFileAsync([FromForm] IFormFile file, DateTime date)
        {

            //var rootFolder = @"E:\Files";//@"D:\Files";

            //var fileName = file.FileName;
            //var filePath = Path.Combine(rootFolder, fileName);
            //var fileLocation = new FileInfo(filePath);

            //using (var fileStream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(fileStream);
            //}


            var employeeCounts = 0;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {

                ExcelWorksheet workSheet = package.Workbook.Worksheets["Summary"];
                //var rootFolder = appConfig.Value.FileLocationConfig.Location;//@"D:\Files";

                if (workSheet == null)
                {
                    return new CreateEmployeeFromFileResult
                    {
                        Success = false,
                        Message = String.Format("ფაილის შაბლონი არასწორია! დამუშავება ვერ მოხერხდა!")
                    };
                }
                int totalRows = workSheet.Dimension.Rows;
                var rowLength = workSheet.Dimension.End.Row;


                List<FileEmployee> employees = new List<FileEmployee>();

                var componentName = workSheet.Cells[3, 10].Value.ToString();

                for (int i = 3; i <= rowLength; i++)
                {
                    var cc = workSheet.Cells[i, 9].Value?.ToString();
                    FileEmployee fileEmployee = new FileEmployee
                    {
                        Fullname = workSheet.Cells[i, 2].Value?.ToString(),
                        ResId = workSheet.Cells[i, 3].Value?.ToString(),
                        GLAccount = workSheet.Cells[i, 4].Value?.ToString(),
                        Project = workSheet.Cells[i, 5].Value?.ToString(),
                        CostCenter = workSheet.Cells[i, 6].Value?.ToString(),
                        CostUnit = workSheet.Cells[i, 7].Value?.ToString(),
                        PersonalNumber = workSheet.Cells[i, 8].Value?.ToString(),
                        Amount = Decimal.Parse(workSheet.Cells[i, 9].Value?.ToString()),
                        Component = workSheet.Cells[i, 10].Value?.ToString(),
                    };

                    employees.Add(fileEmployee);
                }


                /////////////////////////////////////////////////////////////
                var resIds = employees.Select(x => x.ResId?.Trim()).Distinct().ToList();
                var PerIds = employees.Select(x => x.PersonalNumber?.Trim()).Distinct().ToList();


                var empResIds = _repositoryContext.Employees.Where(r => resIds.Contains(r.ResId.ToString()));
                var empPerIds = _repositoryContext.Employees.Where(r => PerIds.Contains(r.ResId.ToString()));

                ///////////////////////////////////////////////////////////

                //var personalNumbers = employees.Select(x => x.PersonalNumber.Trim()).Distinct().ToList();


                //var employeesPersonalNumbers = _repositoryContext.Employees.Select(r => r.PersonalNumber);
                //var empFromDbNotContains = personalNumbers.Where(r => !employeesPersonalNumbers.Contains(r));

                //if (empFromDbNotContains.Count() > 0)
                //{
                //    return new CreateEmployeeFromFileResult
                //    {
                //        Employees = employees.Where(r => empFromDbNotContains.Contains(r.PersonalNumber)).ToList(),
                //        Success = false

                //    };
                //}
                //var empFromDb = _repositoryContext.Employees
                //            .Where(r => personalNumbers.Contains(r.PersonalNumber))
                //            .ToList();


                var grossComponent = _repositoryContext.Components.First(r => r.Code.Trim() == componentName.Trim());

                //var empComp = _repositoryContext.EmployeeComponents.
                var currentDate = DateTime.Now;

                List<Calculation> calculations = new List<Calculation>();

                foreach (var item in empResIds)
                {
                    var emp = employees.FirstOrDefault(r => r.PersonalNumber == item.PersonalNumber
                                                && Int16.Parse(r.ResId) == item.ResId);
                    if (emp != null)
                    {
                        var calculation = GetCalculationObjectForFile(date, grossComponent, item, emp.Amount);
                        calculations.Add(calculation);
                        employeeCounts++;
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


            return new CreateEmployeeFromFileResult
            {
                Success = true,
                Message = String.Format("კალკულაცია წარმატებით დასრულდა {0} თანამშრომელზე", employeeCounts)
            };

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
            calculation.CompType = component.Type;

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

        public class DeleteParamsByFiler
        {
            public List<Guid> employeeIds { get; set; }
            public DateTime? CalculationPeriod { get; set; }
        }

        [HttpPost]
        [Route("deleteCalculationsByFiler")]
        public Result DeleteCalculationsByFiler([FromBody] DeleteParamsByFiler deleteParams)
        {
            //var items = _repositoryContext.Calculations.Where(r => r.EmployeeId == deleteParams.Id);

            //var employees = _repository.Employee.GetCalculationByFilter(calculationFilter);
            //var employees = _repositoryContext.Employees.Where(r => deleteParams.employeeIds.Contains(r.Id)).Include(r => r.Calculations);

            //var calculations = new List<Calculation>();
            //foreach (var employee in employees)
            //{
            //    calculations.AddRange(employee.Calculations);
            //    if (employee != null)
            //    {
            //        employee.RemainingGraceAmount = employee.GraceAmount;
            //    }
            //}



            //_repositoryContext.Employees.UpdateRange(employees);
            //_repositoryContext.Calculations.RemoveRange(calculations);
            //_repositoryContext.SaveChanges();
            //_logger.LogInfo($"Calculation deleted");

            foreach (var item in deleteParams.employeeIds)
            {
                deleteCalculationsByFilter(new DeleteParams { Id = item, CalculationPeriod = deleteParams.CalculationPeriod });
            }

            return new Result(true, 1, String.Format("წარმატებით დასრულდა {0} თანამშრომელზე.", deleteParams.employeeIds.Count()));

        }

        public Result deleteCalculationsByFilter(DeleteParams deleteParams)
        {
            var items = _repositoryContext.Calculations.Where(r => r.EmployeeId == deleteParams.Id);
            var employee = _repositoryContext.Employees.Where(r => r.Id == deleteParams.Id).FirstOrDefault();

            if (deleteParams.CalculationPeriod != null)
            {
                items = items.Where(r => r.PayrollYear == deleteParams.CalculationPeriod.Value.Year &&
                                r.PayrollMonth == deleteParams.CalculationPeriod.Value.Month);


                var nextCalculations = _repositoryContext.Calculations.Where(r => r.EmployeeId == employee.Id &&
                               r.PayrollYear == deleteParams.CalculationPeriod.Value.Year
                                && r.CalculationDate.Value.Month > deleteParams.CalculationPeriod.Value.Month)
                               .OrderBy(r => r.CalculationDate).ThenBy(r => r.DateCreated);

                var lastCalc = _repositoryContext.Calculations.Where(r => r.EmployeeId == employee.Id &&
                               r.PayrollYear == deleteParams.CalculationPeriod.Value.Year
                                && r.CalculationDate.Value.Month < deleteParams.CalculationPeriod.Value.Month)
                               .OrderByDescending(r => r.CalculationDate).ThenByDescending(r => r.DateCreated)
                               .FirstOrDefault();

                var correctRemaining = employee.GraceAmount;
                if (lastCalc == null)
                {
                    correctRemaining = employee.GraceAmount;
                }
                else
                {
                    correctRemaining = lastCalc.RemainingGraceAmount;
                }
                if (employee == null)
                {
                    return new Result(false, 0, "Employee Null");
                }

                if (!nextCalculations.Any())
                {
                    employee.RemainingGraceAmount = correctRemaining;
                }

                foreach (var calc in nextCalculations)
                {
                    var employeeRemainingGraceAmount = employee.RemainingGraceAmount == null ? 0 : employee.RemainingGraceAmount;
                    if (correctRemaining > 0)
                    {
                        calc.IncomeTax = 0;
                        calc.Net = calc.Gross - calc.PensionTax;
                        calc.RemainingGraceAmount = correctRemaining - (calc.Gross - calc.PensionTax);
                        employee.RemainingGraceAmount = correctRemaining - (calc.Gross - calc.PensionTax); ;
                    }
                    else
                    {
                        calc.IncomeTax = (decimal)((calc.Gross - calc.PensionTax - employeeRemainingGraceAmount) / 5);
                        calc.Net = calc.Gross - calc.PensionTax - calc.IncomeTax;
                        employee.RemainingGraceAmount = 0;
                        calc.RemainingGraceAmount = 0;
                    }
                    correctRemaining = correctRemaining - (calc.Gross - calc.PensionTax);
                }

                _repositoryContext.Calculations.UpdateRange(nextCalculations);


            }

            if (deleteParams.CalculationPeriod == null)
            {
                employee.RemainingGraceAmount = employee.GraceAmount;
            }

            _repositoryContext.Employees.Update(employee);
            _repositoryContext.Calculations.RemoveRange(items);
            _repositoryContext.SaveChanges();
            _logger.LogInfo($"Calculation deleted");

            return new Result(true, 1, "წარმატებით დასრულდა");
        }
        public class DeleteParams
        {
            public Guid Id { get; set; }
            public DateTime? CalculationPeriod { get; set; }
        }
        [HttpDelete]
        [Route("deleteCalculations")]
        public Result DeleteCalculations([FromQuery] DeleteParams deleteParams)
        {

            deleteCalculationsByFilter(deleteParams);
            return new Result(true, 0, "წარმატებით დასრულდა !");

        }



        [HttpDelete]
        [Route("delete/{id}")]
        public Result DeleteCalculation(Guid id)
        {

            var item = _repositoryContext.Calculations.Where(r => r.Id == id).FirstOrDefault();
            //item.DateDeleted = DateTime.Now;

            var nextCalculations = _repositoryContext.Calculations.Where(r => r.EmployeeId == item.EmployeeId &&
                                r.PayrollYear == item.PayrollYear
                                && ((r.CalculationDate > item.CalculationDate)
                                    || (r.CalculationDate >= item.CalculationDate && r.DateCreated > item.DateCreated)))
                                .OrderBy(r => r.CalculationDate).ThenBy(r => r.DateCreated);


            var employee = _repositoryContext.Employees.Where(r => r.Id == item.EmployeeId).FirstOrDefault();

            var lastCalc = _repositoryContext.Calculations.Where(r => r.EmployeeId == item.EmployeeId
                        && r.PayrollYear == item.PayrollYear
                       && ((r.CalculationDate < item.CalculationDate)
                       || (r.CalculationDate <= item.CalculationDate && r.DateCreated < item.DateCreated)))
                       .OrderByDescending(r => r.CalculationDate).ThenByDescending(r => r.DateCreated)
                                  .FirstOrDefault();

            var correctRemaining = employee.GraceAmount;
            if (lastCalc == null)
            {
                correctRemaining = employee.GraceAmount;
            }
            else
            {
                correctRemaining = lastCalc.RemainingGraceAmount;
            }
            if (employee == null)
            {
                return new Result(false, 0, "Employee Null");
            }

            if (!nextCalculations.Any())
            {
                employee.RemainingGraceAmount = correctRemaining;
            }

            foreach (var calc in nextCalculations)
            {
                var employeeRemainingGraceAmount = employee.RemainingGraceAmount == null ? 0 : employee.RemainingGraceAmount;
                if (correctRemaining > 0)
                {
                    calc.IncomeTax = 0;
                    calc.Net = calc.Gross - calc.PensionTax;
                    calc.RemainingGraceAmount = correctRemaining - (calc.Gross - calc.PensionTax);
                    employee.RemainingGraceAmount = correctRemaining - (calc.Gross - calc.PensionTax); ;
                }
                else
                {
                    calc.IncomeTax = (decimal)((calc.Gross - calc.PensionTax - employeeRemainingGraceAmount) / 5);
                    calc.Net = calc.Gross - calc.PensionTax - calc.IncomeTax;
                    employee.RemainingGraceAmount = 0;
                    calc.RemainingGraceAmount = 0;
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

            var bankAccounts = paidDTO.Persons.Select(a => a.BankAccountNumber);

            var existingBankAccountsEmployee = _repositoryContext.Employees.Where(r => r.DateDeleted == null &&
                                   bankAccounts.Contains(r.BankAccountNumber));

            var existingBankAccounts = existingBankAccountsEmployee.Select(p => p.BankAccountNumber);

            List<string> nonExistingBankAccounts = bankAccounts
                                                    .Except(existingBankAccounts)
                                                    .ToList();


            if (nonExistingBankAccounts.Count() > 0)
            {
                return new Result(false, 10, string.Join(", ", nonExistingBankAccounts));
            }

            List<string> nonExistingPersonalNumbers = new List<string>();
            var empList = paidDTO.Persons;
            foreach (var emp in existingBankAccountsEmployee)
            {
                var ex = empList.FirstOrDefault(r => r.BankAccountNumber == emp.BankAccountNumber);

                if (ex != null)
                {
                    if (emp.PersonalNumber.Trim() != ex.PersonalNumber.Trim())
                    {
                        nonExistingPersonalNumbers.Add(emp.PersonalNumber.Trim());
                    }
                }
            }

            if (nonExistingPersonalNumbers.Count() > 0)
            {
                return new Result(false, 20, string.Join(", ", string.Join(", ", nonExistingPersonalNumbers)));
            }

            _repository.Calculation.Paid(paidDTO);

            _logger.LogInfo($"Created new Employee.");

            return new Result(true, 1, "წარმატებით დასრულდა");

        }

        public string getBiki(string bankAccountNumber)
        {
            if (bankAccountNumber.Contains("TB"))
            {
                return "TBCBGE22";
            }
            if (bankAccountNumber.Contains("BG"))
            {
                return "BAGAGE22";
            }
            if (bankAccountNumber.Contains("BS"))
            {
                return "CBASGE22";
            }
            if (bankAccountNumber.Contains("CR"))
            {
                return "CRTUGE22";
            }
            if (bankAccountNumber.Contains("LB"))
            {
                return "LBRTGE22";
            }
            if (bankAccountNumber.Contains("PC"))
            {
                return "MIBGGE22";
            }
            if (bankAccountNumber.Contains("VT"))
            {
                return "UGEBGE22";
            }
            if (bankAccountNumber.Contains("PB"))
            {
                return "PAHAGE22";
            }

            return "BNLNGE22";


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

            if (calculationFilter.NotIncludes != null && calculationFilter.NotIncludes.Count() > 0)
            {
                employees = employees.Where(r => !calculationFilter.NotIncludes.Contains(r.Id));
            }

            foreach (var employee in employees)
            {
                decimal paids = 0;
                decimal nets = 0;
                foreach (var item in employee.Calculations)
                {
                    //if (item.EmployeeComponent == null)
                    //{
                    //    continue;
                    //}
                    //if (item.EmployeeComponent.Component.Name.ToLower().Contains("paid"))
                    //{
                    //    paids += item.Paid;
                    //}

                    nets += item.Net;
                }
                workSheet.Cells[recordIndex, 1].Value = employee.BankAccountNumber;
                workSheet.Cells[recordIndex, 2].Value = getBiki(employee.BankAccountNumber); // biki
                workSheet.Cells[recordIndex, 3].Value = String.Format("{0} {1} - ({2})", employee.FirstName, employee.LastName, employee.ResId);
                workSheet.Cells[recordIndex, 4].Value = "xelfasi"; //  daniSnuleba
                workSheet.Cells[recordIndex, 5].Value = paids + nets;
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

        public decimal getToTalBalance(EmployeeDTO employeeDTO)
        {
            decimal paids = 0;
            foreach (var item in employeeDTO.Calculations)
            {
                if (item.EmployeeComponent != null && item.EmployeeComponent.Component.Name.ToLower().Contains("paid"))
                {
                    paids += item.Paid;
                }
            }

            decimal sum = employeeDTO.Calculations.Sum(r => r.Net);

            return sum + paids;
        }

        [HttpGet]
        [Route("downloadCalculations")]
        public FileStreamResult DownloadCalculations([FromQuery] CalculationFilter calculationFilter)
        {


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();

            // name of the sheet
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");


            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;


            // Header of the Excel sheet
            workSheet.Cells[1, 1].Value = "ResId";
            workSheet.Cells[1, 2].Value = "Full Name";
            workSheet.Cells[1, 3].Value = "Gross";
            workSheet.Cells[1, 4].Value = "Net";
            workSheet.Cells[1, 5].Value = "Paid";
            workSheet.Cells[1, 6].Value = "IncomeTax";
            workSheet.Cells[1, 7].Value = "PensionTax";
            workSheet.Cells[1, 8].Value = "Deduction";
            workSheet.Cells[1, 9].Value = "RemainingGraceAmount";
            workSheet.Cells[1, 10].Value = "Total Balance";


            var employees = _repository.Employee.GetCalculationByFilter(calculationFilter);


            int recordIndex = 3;

            IEnumerable<EmployeeDTO> employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);



            foreach (var employee in employeeDTOs)
            {

                workSheet.Cells[recordIndex, 1].Value = employee.ResId;
                workSheet.Cells[recordIndex, 2].Value = String.Format("{0} {1}", employee.FirstName, employee.LastName);
                workSheet.Cells[recordIndex, 3].Value = employee.Calculations.Sum(r => r.Gross);
                workSheet.Cells[recordIndex, 4].Value = employee.Calculations.Sum(r => r.Net);
                workSheet.Cells[recordIndex, 5].Value = employee.Calculations.Sum(r => r.Paid);
                workSheet.Cells[recordIndex, 6].Value = employee.Calculations.Sum(r => r.PensionTax);
                workSheet.Cells[recordIndex, 7].Value = employee.Calculations.Sum(r => r.IncomeTax);
                workSheet.Cells[recordIndex, 8].Value = employee.Calculations.Sum(r => r.EmployeeComponent?.Component.Type == 2 ? r.Net : 0);
                workSheet.Cells[recordIndex, 9].Value = employee.RemainingGraceAmount;
                workSheet.Cells[recordIndex, 10].Value = getToTalBalance(employee);
                recordIndex++;
            }


            var stream = new MemoryStream(excel.GetAsByteArray());

            var mimeType = "Application/vnm.ms-excel";
            var fileStreamResult = new FileStreamResult(stream, mimeType);
            fileStreamResult.FileDownloadName = string.Format("To Bank BOG {0:yyyy_MM_dd_HH_mm_ss}.xlsx", DateTime.Now);

            stream.Seek(0, SeekOrigin.Begin);


            return fileStreamResult;

        }




    }
}
