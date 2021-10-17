using Contracts;
using Entities;
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

        public void CreateEmployee(Employee employee)
        {

            //var lastEmpoyee = this.RepositoryContext.Employees.OrderByDescending(r => r.ResId).FirstOrDefault();
            //int newResId = 1;
            //if (lastEmpoyee != null)
            //{
            //    newResId = lastEmpoyee.ResId;
            //}
            //newResId++;

            employee.Id = Guid.NewGuid();
            employee.DateCreated = DateTime.Now;
            //employee.ResId = newResId;

            foreach (var item in employee.EmployeeComponents)
            {
                item.Id = Guid.NewGuid();
                item.DateCreated = DateTime.Now;
                this.RepositoryContext.EmployeeComponents.Add(item);
            }


            Create(employee);

            Save();
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

        public IEnumerable<Employee> GetAllEmployees()
        {
            return GetAllIncluded(r => r.Department)
                    .Where(r => r.DateDeleted == null)
                .OrderByDescending(r => r.DateCreated);
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
                    .Include(o => o.EmployeeComponents.Where(e => e.DateDeleted == null))
                    .Where(r => r.DateDeleted == null && r.Id == id)
                    .OrderByDescending(r => r.DateCreated).FirstOrDefault();

            return emps;
        }

        public void ImportEmployee(List<Humre> humres)
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
                        BankAccountNumber = "BankN00000000000001",
                        PersonalNumber = item.SocsecNr,
                        MobilePhone = item.TelnrPrv,
                        DateCreated = DateTime.Now,
                        LandIso = item.LandIso
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

            foreach (var item in employee.EmployeeComponents)
            {
                var empComp = RepositoryContext.EmployeeComponents.Where(r => r.Id == item.Id).FirstOrDefault();

                if (empComp == null)
                {
                    item.Id = Guid.NewGuid();
                    item.EmployeeId = employee.Id;
                    item.DateCreated = DateTime.Now;
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
