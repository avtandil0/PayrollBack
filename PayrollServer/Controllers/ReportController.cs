using Entities;
using Entities.FilterModels;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PayrollServer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    //[ApiController]
    public class ReportController : ControllerBase
    {
        private RepositoryContext _repository;
        public ReportController(RepositoryContext repository)
        {
            _repository = repository;
        }

       
        [HttpGet]
        [Route("getReport")]
        public IEnumerable<Calculation> GetReport(CalculationFilter calculationFilter)
        {
            //var query = _repository.Employees.Where(r => r.DateDeleted == null);

            //if (!string.IsNullOrEmpty(calculationFilter.FirstName))
            //{
            //    query = query.Where(r => r.FirstName.Contains(calculationFilter.FirstName));
            //}

            //if (!string.IsNullOrEmpty(calculationFilter.LastName))
            //{
            //    query = query.Where(r => r.LastName.Contains(calculationFilter.LastName));
            //}

            //if(calculationFilter.DepartmentId != null && calculationFilter.DepartmentId.Count() > 0)
            //{
            //    query = query.Where(r => calculationFilter.DepartmentId.Contains((Guid)r.DepartmentId));
            //}

            //if (calculationFilter.CalculationPeriod != null)
            //{
            //    query = query.Where(r => r.Calculations
            //                .Any(c => c.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
            //                    && c.PayrollYear == calculationFilter.CalculationPeriod.Value.Year));

            //    //query = query.Include(r => r.Calculations.Where(c => c.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
            //    //     && c.PayrollYear == calculationFilter.CalculationPeriod.Value.Year));
            //    //query = query.Include(r => r.Calculations).Where(c => c.Calculations
            //    //            .Where(k => k.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
            //    //                     && k.PayrollYear == calculationFilter.CalculationPeriod.Value.Year)));
            //}

            //var data = query.Include(r => r.EmployeeComponents.Where(k => k.DateDeleted == null))
            //                .ThenInclude(r => r.Component).AsNoTracking().Where(d => d.DateDeleted == null);
            //var dataC = data.Count();
            //return data;

            var data = _repository.Calculations.Where(r => r.DateDeleted == null);

            if(calculationFilter.CalculationPeriod != null)
            {
                data = data.Where(r => r.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
                                && r.PayrollYear == calculationFilter.CalculationPeriod.Value.Year);
            }

            if (!string.IsNullOrEmpty(calculationFilter.FirstName))
            {
                data = data.Include(r => r.Employee).Where(r => r.Employee.FirstName.Contains(calculationFilter.FirstName));
            }

            if (!string.IsNullOrEmpty(calculationFilter.LastName))
            {
                data = data.Include(r => r.Employee).Where(r => r.Employee.LastName.Contains(calculationFilter.LastName));
            }

            if(calculationFilter.DepartmentId != null && calculationFilter.DepartmentId.Count() > 0)
            {
                data = data.Include(r => r.Employee).Where(r => calculationFilter.DepartmentId.Contains((Guid)r.Employee.DepartmentId));
            }


            return data.Include(r => r.Employee).ThenInclude(r => r.EmployeeComponents);
        }

        [HttpGet]
        [Route("downloadReport")]
        public FileStreamResult DownloadReport([FromQuery] CalculationFilter calculationFilter)
        {


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();

            // name of the sheet
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");


            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;


            // Header of the Excel sheet
            workSheet.Cells[1, 1].Value = "ResId";
            workSheet.Cells[1, 2].Value = "პირადი ნომერი";
            workSheet.Cells[1, 3].Value = "სახელი";
            workSheet.Cells[1, 4].Value = "გვარი";
            workSheet.Cells[1, 5].Value = "შვებულება";
            workSheet.Cells[1, 6].Value = "PAID";
            workSheet.Cells[1, 7].Value = "Bonus";
            workSheet.Cells[1, 9].Value = "Sum";


            //var employees = _repository.Employee.GetCalculationByFilter(calculationFilter);


            //int recordIndex = 3;

            //IEnumerable<EmployeeDTO> employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);



            //foreach (var employee in employeeDTOs)
            //{

            //    workSheet.Cells[recordIndex, 1].Value = employee.ResId;
            //    workSheet.Cells[recordIndex, 2].Value = String.Format("{0} {1}", employee.FirstName, employee.LastName);
            //    workSheet.Cells[recordIndex, 3].Value = employee.Calculations.Sum(r => r.Gross);
            //    workSheet.Cells[recordIndex, 4].Value = employee.Calculations.Sum(r => r.Net);
            //    workSheet.Cells[recordIndex, 5].Value = employee.Calculations.Sum(r => r.Paid);
            //    workSheet.Cells[recordIndex, 6].Value = employee.Calculations.Sum(r => r.PensionTax);
            //    workSheet.Cells[recordIndex, 7].Value = employee.Calculations.Sum(r => r.IncomeTax);
            //    workSheet.Cells[recordIndex, 8].Value = employee.Calculations.Sum(r => r.EmployeeComponent.Component.Type == 2 ? r.Net : 0);
            //    workSheet.Cells[recordIndex, 9].Value = employee.RemainingGraceAmount;
            //    workSheet.Cells[recordIndex, 10].Value = getToTalBalance(employee);
            //    recordIndex++;
            //}


            var stream = new MemoryStream(excel.GetAsByteArray());

            var mimeType = "Application/vnm.ms-excel";
            var fileStreamResult = new FileStreamResult(stream, mimeType);
            fileStreamResult.FileDownloadName = string.Format("To Bank BOG {0:yyyy_MM_dd_HH_mm_ss}.xlsx", DateTime.Now);

            stream.Seek(0, SeekOrigin.Begin);


            return fileStreamResult;

        }


    }
}
