using Contracts;
using Entities;
using Entities.Models;
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
            employee.Id = Guid.NewGuid();
            employee.DateCreated = DateTime.Now;
            Create(employee);

            foreach (var item in employee.EmployeeComponents)
            {
                item.Id = Guid.NewGuid();
                item.DateCreated = DateTime.Now;
                this.RepositoryContext.EmployeeComponents.Add(item);
            }

            Save();
        }

        public void DeleteEmployee(Employee employee)
        {
            var emp = FindByCondition(r => r.Id == employee.Id).FirstOrDefault();

            emp.DateDeleted = DateTime.Now;

            foreach (var item in employee.EmployeeComponents)
            {
                item.DateDeleted = DateTime.Now; ;
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

        public void UpdateEmployee(Employee employee)
        {
            var emp = FindByCondition(r => r.Id == employee.Id).FirstOrDefault();

            emp.DateChange = DateTime.Now;
            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.MobilePhone = employee.MobilePhone;
            emp.Email = employee.Email;
            emp.Address = employee.Address;
            emp.PersonalNumber = emp.PersonalNumber;
            emp.BankAccountNumber = emp.BankAccountNumber;
            emp.DepartmentId = emp.DepartmentId;

            //foreach (var item in employee.EmployeeComponents)
            //{
            //    item.DateChange = DateTime.Now;

            //    item.EmployeeId = employee.EmployeeComponents.EmployeeId;
            //    item.ComponentId = employeeComponent.ComponentId;
            //    item.ProjectId = employeeComponent.ProjectId;
            //    item.CostCenterId = employeeComponent.CostCenterId;
            //    item.Days = employeeComponent.Days;
            //    item.StartDate = employeeComponent.StartDate;
            //    item.EndDate = employeeComponent.EndDate;
            //    item.Scheme = employeeComponent.Scheme;
            //    item.Amount = employeeComponent.Amount;
            //    item.Currency = employeeComponent.Currency;
            //    item.PaidByCash = employeeComponent.PaidByCash;
            //    item.CashAmount = employeeComponent.CashAmount;
            //    item.PaidMultiple = employeeComponent.PaidMultiple;
            //}


            Update(emp);
            Save();
        }
    }
}
