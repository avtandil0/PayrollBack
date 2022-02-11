using Contracts;
using Entities;
using Entities.Enumerations;
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
    public class CalculationRepository : RepositoryBase<Calculation>, ICalculationRepository
    {
        //private IEmployeeComponentsRepository employeeComponentsRepository;

        public CalculationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateCalculation(CalculationFilter calculationFilter, DateTime calculationDate)
        {


            var employees = RepositoryContext.Employees.Include(r => r.EmployeeComponents.Where(r => r.DateDeleted == null)).Where(r => r.DateDeleted == null);

            if (!string.IsNullOrEmpty(calculationFilter.FirstName))
            {
                employees = employees.Where(r => r.FirstName.Contains(calculationFilter.FirstName));
            }

            if (!string.IsNullOrEmpty(calculationFilter.LastName))
            {
                employees = employees.Where(r => r.FirstName.Contains(calculationFilter.LastName));
            }

            var currentTime = DateTime.Now;
            foreach (var emp in employees)
            {
                foreach (var empComp in emp.EmployeeComponents)
                {
                    var component = RepositoryContext.Components.Where(r => r.Id == empComp.ComponentId && r.DateDeleted == null).FirstOrDefault();

                    if (empComp.StartDate > currentTime || empComp.EndDate < currentTime)
                    {
                        continue;
                    }

                    if (empComp.PaidMultiple == false)
                    {
                        var currentMonthCalculations = RepositoryContext.Calculations
                                        .Where(r => r.EmployeeId == emp.Id && r.PayrollYear == calculationDate.Year
                                                    && r.PayrollMonth == calculationDate.Month);

                        if (currentMonthCalculations.Count() > 0)
                        {
                            continue;
                        }
                    }

                    var coefficient = RepositoryContext.Coefficients.Where(r => r.Id == component.CoefficientId && r.DateDeleted == null).FirstOrDefault();

                    Calculation calculation = new Calculation();
                    calculation.Id = Guid.NewGuid();
                    calculation.CalculationDate = calculationDate;
                    calculation.DateCreated = DateTime.Now;

                    calculation.EmployeeId = emp.Id;
                    calculation.EmployeeComponentId = empComp.Id;
                    calculation.PayrollYear = calculationDate.Year;
                    calculation.PayrollMonth = calculationDate.Month;
                    calculation.SchemeTypeId = empComp.SchemeTypeId;
                    calculation.RemainingGraceAmount = 0;

                    if (emp.SchemeTypeId == (int)SchemeTypeEnum.Standart)
                    {
                        calculation.Gross = empComp.Amount * (decimal)coefficient.Sgross;
                        calculation.Net = empComp.Amount * (decimal)coefficient.Snet;
                        calculation.Paid = empComp.Amount * (decimal)coefficient.Spaid;
                        calculation.PensionTax = empComp.Amount * (decimal)coefficient.Spension;
                        calculation.IncomeTax = empComp.Amount * (decimal)coefficient.SincomeTax;
                    }

                    if (emp.SchemeTypeId == (int)SchemeTypeEnum.Pension)
                    {
                        calculation.Gross = empComp.Amount * (decimal)coefficient.Pgross;
                        calculation.Net = empComp.Amount * (decimal)coefficient.Pnet;
                        calculation.Paid = empComp.Amount * (decimal)coefficient.Ppaid;
                        calculation.PensionTax = empComp.Amount * (decimal)coefficient.Ppension;
                        calculation.IncomeTax = empComp.Amount * (decimal)coefficient.PincomeTax;
                    }

                    if (emp.RemainingGraceAmount > 0)
                    {
                        var rem = emp.RemainingGraceAmount - (calculation.Gross - calculation.PensionTax);
                        if (rem > 0)
                        {
                            emp.RemainingGraceAmount = rem;
                            calculation.RemainingGraceAmount = rem;
                            calculation.IncomeTax = 0;
                            calculation.Net = calculation.Gross - calculation.PensionTax;
                        }
                        else
                        {
                            calculation.IncomeTax = (decimal)((calculation.Gross - calculation.PensionTax - emp.RemainingGraceAmount) / 5);
                            calculation.Net = calculation.Gross - calculation.PensionTax - calculation.IncomeTax;
                            emp.RemainingGraceAmount = 0;
                            calculation.RemainingGraceAmount = 0;
                        }

                    }

                    Create(calculation);
                }
            }



            Save();
        }

        public void DeleteCalculation(Calculation calculation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Calculation> GetAllCalculation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Calculation> GetCalculationByFilter(CalculationFilter calculationFilter)
        {
            var query = RepositoryContext.Calculations
                   .Include(o => o.Employee)
                   .Include(o => o.EmployeeComponent)
                   .Where(r => r.DateDeleted == null);


            if (!string.IsNullOrEmpty(calculationFilter.FirstName))
            {
                query = query.Where(r => r.Employee.FirstName.Contains(calculationFilter.FirstName));
            }

            if (!string.IsNullOrEmpty(calculationFilter.LastName))
            {
                query = query.Where(r => r.Employee.FirstName.Contains(calculationFilter.LastName));
            }

            return query.OrderByDescending(r => r.DateCreated);
        }

        public void Paid(PaidHelper paidHelper)
        {
            var bankAccounts = paidHelper.Persons.Select(a => a.BankAccountNumber);

            var employees = RepositoryContext.Employees.Where(r => r.DateDeleted == null &&
                                    bankAccounts.Contains(r.BankAccountNumber));

            //var test1 = from emp in RepositoryContext.Employees
            //            join per in paidHelper.Persons on emp.BankAccountNumber equals per.BankAccountNumber
            //            select new { OwnerName = per.BankAccountNumber, PetName = per.Amount }; 

            //var test2 = test1.ToList();
            var test = employees.ToList();
            var component = RepositoryContext.Components.Where(r => r.DateDeleted != null);

            //var employees = RepositoryContext.Employees.Where(r => paidHelper.Persons.Any(a => a.BankAccountNumber == r.BankAccountNumber));




            var employeeComponent = new EmployeeComponent();
            var currentTime = DateTime.Now;

            foreach (var item in employees)
            {
                employeeComponent.Id = Guid.NewGuid();
                employeeComponent.DateCreated = currentTime;
                employeeComponent.ComponentId = paidHelper.ComponentID;

            }


            throw new NotImplementedException();
        }

        public void UpdateCalculation(Calculation calculation)
        {
            throw new NotImplementedException();
        }

        public Calculation GetCalculationObject(Coefficient coefficient)
        {
            throw new NotImplementedException();
        }
    }
}
