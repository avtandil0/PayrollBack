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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository
{
    public class CalculationRepository : RepositoryBase<Calculation>, ICalculationRepository
    {
        //private IEmployeeComponentsRepository employeeComponentsRepository;

        public CalculationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IQueryable<Employee> GetEmployeeByFilter(CalculationFilter calculationFilter, bool withCalculations=true)
        {
            var employees =  RepositoryContext.Employees.Include(r => r.Calculations.Where(r => r.DateDeleted == null).OrderByDescending(r => r.CalculationDate))
                                                     .Include(r => r.EmployeeComponents.Where(r => r.DateDeleted == null))
                                                     .ThenInclude(r => r.Component).Where(r => r.DateDeleted == null);

            if (!string.IsNullOrEmpty(calculationFilter.FirstName))
            {
                employees = employees.Where(r => r.FirstName.Contains(calculationFilter.FirstName));
            }

            if (!string.IsNullOrEmpty(calculationFilter.LastName))
            {
                employees = employees.Where(r => r.LastName.Contains(calculationFilter.LastName));
            }

            if (calculationFilter.DepartmentId != null && calculationFilter.DepartmentId.Count() > 0)
            {
                employees = employees.Where(r => calculationFilter.DepartmentId.Contains((Guid)r.DepartmentId));
            }

            if (withCalculations == true && calculationFilter.CalculationPeriod != null)
            {
                employees = employees.Where(r => r.Calculations
                            .Any(c => c.PayrollMonth == calculationFilter.CalculationPeriod.Value.Month
                                && c.PayrollYear == calculationFilter.CalculationPeriod.Value.Year));


            }

            return employees;
        }

        public void CreateCalculationWithComponent(CalculationFilter calculationFilter, DateTime calculationDate, Guid componentId,
             decimal amount, int currency)
        {


            var employees = GetEmployeeByFilter(calculationFilter, false);

            var currentTime = DateTime.Now;
            foreach (var emp in employees)
            {
                var component = RepositoryContext.Components.Where(r => r.Id == componentId && r.DateDeleted == null).FirstOrDefault();

                var calculation = GetCalculationObject(calculationDate, component.CoefficientId, emp, null, component, amount, currency);

                Create(calculation);

            }



            Save();
        }
        public void CreateCalculation(CalculationFilter calculationFilter, DateTime calculationDate)
        {

            var query = GetEmployeeByFilter(calculationFilter, false);


            var currentTime = DateTime.Now;
            foreach (var emp in query)
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

                        if (currentMonthCalculations.Any())
                        {
                            continue;
                        }
                    }


                    var calculation = GetCalculationObject(calculationDate, component.CoefficientId, emp, empComp, null, null, null);

                    Create(calculation);

                    //var coefficient = RepositoryContext.Coefficients.Where(r => r.Id == component.CoefficientId && r.DateDeleted == null).FirstOrDefault();

                    //Calculation calculation = new Calculation();
                    //calculation.Id = Guid.NewGuid();
                    //calculation.CalculationDate = calculationDate;
                    //calculation.DateCreated = DateTime.Now;

                    //calculation.EmployeeId = emp.Id;
                    //calculation.EmployeeComponentId = empComp.Id;
                    //calculation.PayrollYear = calculationDate.Year;
                    //calculation.PayrollMonth = calculationDate.Month;
                    //calculation.SchemeTypeId = empComp.SchemeTypeId;
                    //calculation.RemainingGraceAmount = 0;

                    //if (emp.SchemeTypeId == (int)SchemeTypeEnum.Standart)
                    //{
                    //    calculation.Gross = empComp.Amount * (decimal)coefficient.Sgross;
                    //    calculation.Net = empComp.Amount * (decimal)coefficient.Snet;
                    //    calculation.Paid = empComp.Amount * (decimal)coefficient.Spaid;
                    //    calculation.PensionTax = empComp.Amount * (decimal)coefficient.Spension;
                    //    calculation.IncomeTax = empComp.Amount * (decimal)coefficient.SincomeTax;
                    //}

                    //if (emp.SchemeTypeId == (int)SchemeTypeEnum.Pension)
                    //{
                    //    calculation.Gross = empComp.Amount * (decimal)coefficient.Pgross;
                    //    calculation.Net = empComp.Amount * (decimal)coefficient.Pnet;
                    //    calculation.Paid = empComp.Amount * (decimal)coefficient.Ppaid;
                    //    calculation.PensionTax = empComp.Amount * (decimal)coefficient.Ppension;
                    //    calculation.IncomeTax = empComp.Amount * (decimal)coefficient.PincomeTax;
                    //}

                    //if (emp.RemainingGraceAmount > 0)
                    //{
                    //    var rem = emp.RemainingGraceAmount - (calculation.Gross - calculation.PensionTax);
                    //    if (rem > 0)
                    //    {
                    //        emp.RemainingGraceAmount = rem;
                    //        calculation.RemainingGraceAmount = rem;
                    //        calculation.IncomeTax = 0;
                    //        calculation.Net = calculation.Gross - calculation.PensionTax;
                    //    }
                    //    else
                    //    {
                    //        calculation.IncomeTax = (decimal)((calculation.Gross - calculation.PensionTax - emp.RemainingGraceAmount) / 5);
                    //        calculation.Net = calculation.Gross - calculation.PensionTax - calculation.IncomeTax;
                    //        emp.RemainingGraceAmount = 0;
                    //        calculation.RemainingGraceAmount = 0;
                    //    }

                    //}


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




            var currentTime = DateTime.Now;

            foreach (var employee in employees)
            {
                var person = paidHelper.Persons.Where(r => r.BankAccountNumber.Contains(employee.BankAccountNumber)).FirstOrDefault();

                if (person != null)
                {
                    var employeeComponent = new EmployeeComponent();

                    employeeComponent.Id = Guid.NewGuid();
                    employeeComponent.DateCreated = currentTime;
                    employeeComponent.StartDate = currentTime;
                    employeeComponent.EndDate = currentTime;
                    employeeComponent.EmployeeId = employee.Id;
                    employeeComponent.ComponentId = paidHelper.ComponentID;
                    employeeComponent.Amount = Convert.ToDecimal(person.Amount);
                    employeeComponent.Currency = 1;
                    employeeComponent.IsPermanent = false;



                    var component = RepositoryContext.Components.Where(r => r.Id == paidHelper.ComponentID).FirstOrDefault();

                    var calculation = GetCalculationObject(paidHelper.PaidDate, component.CoefficientId, employee, employeeComponent,
                                                                null, null, null);

                    var lastPaid = RepositoryContext.Calculations
                        .Where(r => r.DateDeleted == null && r.EmployeeId == employee.Id && r.EmployeeComponentId == paidHelper.ComponentID)
                        .OrderByDescending(r => r.DateCreated).FirstOrDefault();

                    decimal previousBalance = 0;
                    if (lastPaid != null)
                    {
                        previousBalance = (decimal)lastPaid.TotalBalance;
                    }

                    var currentMonthsCalculations = RepositoryContext.Calculations
                        .Where(r => r.DateDeleted == null && r.CalculationDate.Value.Year == currentTime.Year
                        && r.CalculationDate.Value.Month == currentTime.Month
                        && r.EmployeeComponentId != paidHelper.ComponentID);

                    decimal currentMonthsCalculationsSum = 0;
                    if (currentMonthsCalculations.Count() > 0)
                    {
                        currentMonthsCalculationsSum = currentMonthsCalculations.Sum(r => r.Net);
                    }


                    calculation.TotalBalance = previousBalance +
                        (Convert.ToDecimal(person.Amount) - currentMonthsCalculationsSum);

                    Create(calculation);

                    RepositoryContext.EmployeeComponents.Add(employeeComponent);
                }


            }


            Save();

        }

        public void UpdateCalculation(Calculation calculation)
        {
            throw new NotImplementedException();
        }

        public Calculation GetCalculationObjectForFile(DateTime calculationDate, Component component, Employee employee, decimal amount)
        {
            var coefficient = RepositoryContext.Coefficients.Where(r => r.Id == component.CoefficientId && r.DateDeleted == null).FirstOrDefault();

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
        public Calculation GetCalculationObject(DateTime calculationDate, Guid? coefficientId, Employee employee,
            EmployeeComponent empComp, Component? newComponent, decimal? newAmount, int? newCurrency)
        {

            var coefficient = RepositoryContext.Coefficients.Where(r => r.Id == coefficientId && r.DateDeleted == null).FirstOrDefault();

            Calculation calculation = new Calculation();
            decimal amount = 0;
            if (empComp != null)
            {
                amount = empComp.Amount;
            }
            if (newAmount != null)
            {
                amount = (decimal)newAmount;
            }

            var empCompAmount = amount;
            if (empComp != null)
            {
                if (empComp.PaymentDaysTypeId == (int)Entities.Enumerations.PaymentDaysType.CalendarDay)
                {
                    if (empComp.Component.Type != 2)
                    {
                        var currentDate = DateTime.Now;
                        if ((empComp.StartDate.Month == calculationDate.Month && empComp.StartDate.Year == calculationDate.Year))
                        {
                            var daysInMonth = DateTime.DaysInMonth(empComp.StartDate.Year, empComp.StartDate.Month);
                            var workingDays = daysInMonth - empComp.StartDate.Day;

                            empCompAmount = empComp.Amount / daysInMonth * (workingDays + 1);
                        }
                        if ((empComp.EndDate?.Month == calculationDate.Month && empComp.EndDate?.Year == calculationDate.Year))
                        {
                            var daysInMonth = DateTime.DaysInMonth((int)(empComp.EndDate?.Year), (int)(empComp.EndDate?.Month));
                            var workingDays = daysInMonth - empComp.EndDate?.Day;

                        }
                    }

                }
            }


            empCompAmount = Math.Round(empCompAmount, 2);

            calculation.Id = Guid.NewGuid();
            calculation.CalculationDate = calculationDate;
            calculation.DateCreated = DateTime.Now;

            calculation.EmployeeId = employee.Id;
            Guid? EmployeeComponentId = null;
            if (empComp != null)
            {
                EmployeeComponentId = empComp.Id;
            }
            calculation.EmployeeComponentId = EmployeeComponentId;

            Component? component = null;

            if (newComponent == null)
            {
                component = RepositoryContext.Components.Where(r => r.Id == empComp.ComponentId).FirstOrDefault();
            }
            else
            {
                component = newComponent;
            }

            calculation.ResId = employee.ResId;
            calculation.CompCode = component.Code;

            calculation.PayrollYear = calculationDate.Year;
            calculation.PayrollMonth = calculationDate.Month;
            int? SchemeTypeId = null;
            if (empComp != null)
            {
                SchemeTypeId = empComp.SchemeTypeId;
            }

            calculation.SchemeTypeId = SchemeTypeId;
            calculation.RemainingGraceAmount = 0;

            Rate rate = new Rate();
            decimal exchangeRate = 1;
            if (empComp != null)
            {
                if (empComp.Currency > 1)
                {
                    rate = RepositoryContext.Rates.Include(r => r.Currency).FirstOrDefault(r => r.CurrencyId == empComp.Currency
                                                           && calculationDate == r.Date);
                    if (rate == null)
                    {
                        throw new Exception("გადმოცემული თარიღისთვის კურსი ვერ მოიძებნა");

                    }
                    exchangeRate = (decimal)rate.ExchangeRate;

                }
                else
                {
                    calculation.Currency = empComp.Currency.ToString();
                }
            }
            else
            {
                if (newCurrency > 1)
                {
                    rate = RepositoryContext.Rates.Include(r => r.Currency).FirstOrDefault(r => r.CurrencyId == newCurrency
                                                           && calculationDate == r.Date);
                    if (rate == null)
                    {
                        throw new Exception("გადმოცემული თარიღისთვის კურსი ვერ მოიძებნა");

                    }
                    exchangeRate = (decimal)rate.ExchangeRate;
                }

            }


            if (exchangeRate > 1)
            {
                calculation.GrossForeign = empComp?.Amount;
                calculation.ExchangeRate = rate.ExchangeRate;
            }



            if (empComp != null)
            {
                empComp.Amount = (decimal)(empComp.Amount * exchangeRate);
            }

            if (empComp != null)
            {
                calculation.BaseValue = empComp.Amount;
            }
            else
            {
                calculation.BaseValue = (decimal)newAmount;
            }


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
            //მიმდინარე კალკულაცია
            var correctRemaining = employee.GraceAmount;
            var lastCalc = RepositoryContext.Calculations.Where(r => r.EmployeeId == calculation.EmployeeId
                        && r.PayrollYear == calculation.PayrollYear
                       && r.CalculationDate < calculation.CalculationDate)
                       .OrderByDescending(r => r.CalculationDate)//.ThenBy(r => r.DateCreated)
                                  .FirstOrDefault();
            if (lastCalc == null)
            {
                correctRemaining = employee.GraceAmount;
            }
            else
            {
                correctRemaining = lastCalc.RemainingGraceAmount;
            }

            correctRemaining = correctRemaining == null ? 0 : correctRemaining;

            if (correctRemaining > 0 && calculation.Net > 0 && component.IgnoreIncome == false)
            {
                var rem = correctRemaining - (calculation.Gross - calculation.PensionTax);
                correctRemaining = rem;
                if (rem >= 0)
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

            //მომდევნო კალკულაციები
            var nextCalculations = RepositoryContext.Calculations.Where(r => r.PayrollYear == calculation.PayrollYear
                                && r.CalculationDate > calculation.CalculationDate)
                                .OrderBy(r => r.CalculationDate);

            foreach (var calc in nextCalculations)
            {
                //employee.RemainingGraceAmount = remaining;
                //calc.RemainingGraceAmount = remaining;
                var employeeRemainingGraceAmount = employee.RemainingGraceAmount == null? 0 : employee.RemainingGraceAmount;
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

            RepositoryContext.Calculations.UpdateRange(nextCalculations);

            return calculation;
        }
    }
}
