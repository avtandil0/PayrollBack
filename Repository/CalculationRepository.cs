using Contracts;
using Entities;
using Entities.Enumerations;
using Entities.FilterModels;
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
            Calculation calculation = new Calculation();
            calculation.Id = Guid.NewGuid();
            calculation.CalculationDate = calculationDate;
            calculation.DateCreated = DateTime.Now;

            var employees = RepositoryContext.Employees.Include(r => r.EmployeeComponents).Where(r => r.DateDeleted == null);

            if (!string.IsNullOrEmpty(calculationFilter.FirstName))
            {
                employees = employees.Where(r => r.FirstName.Contains(calculationFilter.FirstName));
            }

            if (!string.IsNullOrEmpty(calculationFilter.LastName))
            {
                employees = employees.Where(r => r.FirstName.Contains(calculationFilter.LastName));
            }

            foreach (var emp in employees)
            {
                foreach (var empComp in emp.EmployeeComponents)
                {
                    var component = RepositoryContext.Components.Where(r => r.Id == empComp.ComponentId).FirstOrDefault();
                    var coefficient = RepositoryContext.Coefficients.Where(r => r.Id == component.CoefficientId).FirstOrDefault();

                    calculation.EmployeeId = emp.Id;
                    calculation.EmployeeComponentId = empComp.Id;
                    calculation.PayrollYear = calculationDate.Year;
                    calculation.PayrollMonth = calculationDate.Month;
                    calculation.SchemeTypeId = empComp.SchemeTypeId;


                    if (empComp.SchemeTypeId == (int)SchemeTypeEnum.Standart)
                    {
                        calculation.Gross = empComp.Amount * (decimal)coefficient.Sgross;
                        calculation.Net = empComp.Amount * (decimal)coefficient.Snet;
                        calculation.Paid = empComp.Amount * (decimal)coefficient.Spaid;
                        calculation.IncomeTax = empComp.Amount * (decimal)coefficient.SincomeTax;
                        calculation.PensionTax = empComp.Amount * (decimal)coefficient.Spension;
                    }

                    if (empComp.SchemeTypeId == (int)SchemeTypeEnum.Pension)
                    {
                        calculation.Gross = empComp.Amount * (decimal)coefficient.Pgross;
                        calculation.Net = empComp.Amount * (decimal)coefficient.Pnet;
                        calculation.Paid = empComp.Amount * (decimal)coefficient.Ppaid;
                        calculation.IncomeTax = empComp.Amount * (decimal)coefficient.PincomeTax;
                        calculation.PensionTax = empComp.Amount * (decimal)coefficient.Ppension;
                    }
                }
            }


            Create(calculation);
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

        public void UpdateCalculation(Calculation calculation)
        {
            throw new NotImplementedException();
        }
    }
}
