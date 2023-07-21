using Contracts;
using Entities;
using Entities.FilterModels;
using Entities.HelperModels;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        //private IEmployeeComponentsRepository employeeComponentsRepository;

        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Guid CreateEmployee(Employee employee)
        {

            var lastEmpoyee = this.RepositoryContext.Employees.OrderByDescending(r => r.ResId).FirstOrDefault();
            int newResId = 1;
            if (lastEmpoyee != null)
            {
                newResId = (int)lastEmpoyee.ResId;
            }
            newResId++;

            employee.Id = Guid.NewGuid();
            employee.DateCreated = DateTime.Now;
            employee.RemainingGraceAmount = employee.GraceAmount;
            employee.ResId = newResId;

            foreach (var item in employee.EmployeeComponents)
            {
                item.Id = Guid.NewGuid();
                item.DateCreated = DateTime.Now;
                item.IsPermanent = true;
                this.RepositoryContext.EmployeeComponents.Add(item);
            }


            Create(employee);

            Save();

            return employee.Id;
        }

        public void DeleteEmployee(Employee employee)
        {
            var emp = FindByCondition(r => r.Id == employee.Id).FirstOrDefault();

            emp.DateDeleted = DateTime.Now;

            foreach (var item in employee.EmployeeComponents)
            {
                item.DateDeleted = DateTime.Now;
            }


            Update(emp);
            Save();
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return GetAllIncluded(r => r.Department, r => r.SchemeType)
                    .Where(r => r.DateDeleted == null)
                .OrderByDescending(r => r.DateCreated);
        }

        public IEnumerable<Employee> GetCalculationByFilter(CalculationFilter calculationFilter)
        {
            //var query = RepositoryContext.Employees
            //       .Include(o => o.Calculations.Where(r => r.PayrollYear == calculationFilter.CalculationPeriod.Value.Year
            //                                            && r.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month))
            //       .Where(r => r.DateDeleted == null)
            //       .Select(emp => new EmployeeCalculationHelper
            //       {
            //          Id = emp.Id,
            //          FirstName = emp.FirstName,
            //          LastName = emp.LastName,
            //          CalculationPeriod = emp.,
            //       });

            //var query = from emp in RepositoryContext.Employees
            //            join empCalc in RepositoryContext.Calculations
            //                on emp.Id equals empCalc.EmployeeId into grouping
            //            from c in grouping.DefaultIfEmpty()
            //             select new EmployeeCalculationHelper
            //             {
            //                 Id = emp.Id,
            //                 FirstName = emp.FirstName,
            //                 LastName = emp.LastName,
            //                 PayrollMonth = c.PayrollMonth.Value,
            //                 PayrollYear = c.PayrollYear.Value,
            //                 CalculationDate = c.CalculationDate.Value,
            //                 Gross = c.Gross,
            //                 Income = c.IncomeTax,
            //                 Pension = c.PensionTax,
            //                 ToBePaid = c.Paid,
            //                 Paid = c.Paid,
            //                 DateChange = c.DateChange.Value,
            //                 //DateCreated = empCalc.DateCreated,
            //                 DateDeleted = c.DateDeleted.Value,
            //             };


            //var query = from emp in RepositoryContext.Employees
            //            from empCalc in emp.Calculations.DefaultIfEmpty()
            //           select new EmployeeCalculationHelper
            //           {
            //               Id = emp.Id,
            //               FirstName = emp.FirstName,
            //               LastName = emp.LastName,
            //               PayrollMonth = empCalc.PayrollMonth.Value,
            //               PayrollYear = empCalc.PayrollYear.Value,
            //               CalculationDate = empCalc.CalculationDate.Value,
            //               Gross = empCalc.Gross,
            //               Income = empCalc.IncomeTax,
            //               Pension = empCalc.PensionTax,
            //               ToBePaid = empCalc.Paid,
            //               Paid = empCalc.Paid,
            //               DateChange = empCalc.DateChange.Value,
            //               //DateCreated = empCalc.DateCreated,
            //               DateDeleted = empCalc.DateDeleted.Value,
            //           };


            var query = RepositoryContext.Employees.Include(r => r.Calculations.Where(r => r.DateDeleted == null).OrderByDescending(r => r.CalculationDate))
                                                    .ThenInclude(r => r.EmployeeComponent)
                                                    .ThenInclude(r => r.Component).Where(r => r.DateDeleted == null).ToList(); 

            if (!string.IsNullOrEmpty(calculationFilter.Name))
            {
                query = query.Where(r => r.FirstName.Contains(calculationFilter.Name) || r.LastName.Contains(calculationFilter.Name)).ToList() ;
            }

            //if (!string.IsNullOrEmpty(calculationFilter.LastName))
            //{
            //    query = query.Where(r => r.LastName.Contains(calculationFilter.LastName)).ToList();
            //}

            if (calculationFilter.DepartmentId != null)
            {
                query = query.Where(r => calculationFilter.DepartmentId.Contains((Guid)r.DepartmentId)).ToList();
            }

            if (calculationFilter.CalculationPeriod != null)
            {
                //query = query.Where(r => r.Calculations
                //            .Any(c => c.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
                //                && c.PayrollYear == calculationFilter.CalculationPeriod.Value.Year));

                query = query.Select(r => new Employee
                {
                    BankAccountNumber = r.BankAccountNumber,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Id = r.Id,
                    Calculations = r.Calculations.Where(c => c.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
                     && c.PayrollYear == calculationFilter.CalculationPeriod.Value.Year).ToList()
                }).ToList();
            }

            return query.OrderByDescending(r => r.DateCreated);
        }

        public IEnumerable<Employee> GetEmployeeByDepartment(Guid depId)
        {
            return RepositoryContext.Employees.Where(r => r.DepartmentId == depId && r.DateDeleted == null);
        }
        
        public void UpdateEmployeeAvatar(EmployeeAvatar avatar)
        {
            var emp = RepositoryContext.Employees.Where(r => r.Id == avatar.UserId).FirstOrDefault();

            if(emp != null)
            {
                emp.Avatar = avatar.File;
                emp.DateChange = DateTime.Now;
            }

            Save();

            //return GetAllIncluded(r => r.Department)
            //        .Where(r => r.DateDeleted == null)
            //    .OrderByDescending(r => r.DateCreated);
        }

        public Employee GetEmployeeById(Guid id)
        {
            //ფილტრი ინქლუდში??
            //var emps = GetAllIncluded(r => r.Department, r => r.SchemeType, r => r.EmployeeComponents)
            //       .Where(r => r.DateDeleted == null && r.Id == id )
            //   .OrderByDescending(r => r.DateCreated).FirstOrDefault();

            var emps = RepositoryContext.Employees
                    .Include(o => o.Department)
                    .Include(o => o.SchemeType)
                    .Include(o => o.EmployeeComponents.Where(e => e.DateDeleted == null && e.IsPermanent == true))
                        .ThenInclude(x => x.Component)
                     .Include(o => o.EmployeeComponents.Where(e => e.DateDeleted == null && e.IsPermanent == true))
                        .ThenInclude(x => x.CostCenter)
                    .Include(o => o.EmployeeComponents.Where(e => e.DateDeleted == null && e.IsPermanent == true))
                        .ThenInclude(x => x.Project)
                    .Include(o => o.EmployeeComponents.Where(e => e.DateDeleted == null && e.IsPermanent == true))
                        .ThenInclude(x => x.SchemeType)
                    .Where(r => r.DateDeleted == null && r.Id == id)
                    .OrderByDescending(r => r.DateCreated).FirstOrDefault();

            return emps;
        }

        public void ImportEmployee(List<HumreHelper> humres)
        {
            foreach (var item in humres)
            {
                var exists = this.RepositoryContext.Employees.Where(r => r.ResId == item.ResId).FirstOrDefault();
                if (exists == null)
                {

                    var employee = new Employee
                    {
                        Id = Guid.NewGuid(),
                        ResId = item.ResId,
                        FirstName = item.FirstName,
                        LastName = item.SurName,
                        Address = item.Adres1,
                        SchemeTypeId = 1,
                        BankAccountNumber = item.BankAccountNumber,
                        PersonalNumber = item.SocsecNr,
                        MobilePhone = item.TelnrPrv,
                        DateCreated = DateTime.Now,
                        LandIso = item.LandIso,
                        Position =item.Position
                    };

                    Create(employee);
                }


            }

            Save();
        }

        public void UpdateEmployee(Employee employee)
        {
            //var emp = FindByCondition(r => r.Id == employee.Id).FirstOrDefault();
            var emp = GetAllIncluded(r => r.EmployeeComponents).Where(r => r.Id == employee.Id && r.DateDeleted == null)
                    .FirstOrDefault();

            emp.DateChange = DateTime.Now;
            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.MobilePhone = employee.MobilePhone;
            emp.Email = employee.Email;
            emp.Address = employee.Address;
            emp.PersonalNumber = employee.PersonalNumber;
            emp.BankAccountNumber = employee.BankAccountNumber;
            emp.DepartmentId = employee.DepartmentId;
            emp.Position = employee.Position;
            emp.LandIso = employee.LandIso;
            emp.EmployeeTypeId = employee.EmployeeTypeId;
            emp.SchemeTypeId = employee.SchemeTypeId;
            emp.EmployeeGraceTypeId = employee.EmployeeGraceTypeId;
            emp.GraceAmount = employee.GraceAmount;

            foreach (var item in employee.EmployeeComponents)
            {
                var empComp = RepositoryContext.EmployeeComponents.Where(r => r.Id == item.Id).FirstOrDefault();

                if (empComp == null)
                {
                    item.Id = Guid.NewGuid();
                    item.EmployeeId = employee.Id;
                    item.DateCreated = DateTime.Now;
                    item.IsPermanent = true;
                    this.RepositoryContext.EmployeeComponents.Add(item);

                    continue;
                }

                item.DateChange = DateTime.Now;

                empComp.EmployeeId = item.EmployeeId;
                empComp.ComponentId = item.ComponentId;
                empComp.ProjectId = item.ProjectId;
                empComp.CostCenterId = item.CostCenterId;
                empComp.PaymentDaysTypeId = item.PaymentDaysTypeId;
                empComp.StartDate = item.StartDate;
                empComp.EndDate = item.EndDate;
                empComp.SchemeTypeId = item.SchemeTypeId;
                empComp.Amount = item.Amount;
                empComp.Currency = item.Currency;
                empComp.PaidByCash = item.PaidByCash;
                empComp.CashAmount = item.CashAmount;
                empComp.PaidMultiple = item.PaidMultiple;
            }

            var deletedList = emp.EmployeeComponents
                    .Where(r => r.DateDeleted == null && !employee.EmployeeComponents.Any(e => e.Id == r.Id));

            foreach (var item in deletedList)
            {
                item.DateDeleted = DateTime.Now;
            }


            Update(emp);
            Save();
        }
    }
}
