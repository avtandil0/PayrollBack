using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class EmployeeComponentsRepository : RepositoryBase<EmployeeComponent>, IEmployeeComponentsRepository
    {
        public EmployeeComponentsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateEmployeeComponent(EmployeeComponent employeeComponent)
        {
            employeeComponent.Id = Guid.NewGuid();
            employeeComponent.DateCreated = DateTime.Now;
            Create(employeeComponent);
            Save();
        }

        public void DeleteEmployeeComponent(EmployeeComponent employeeComponent)
        {
            var empComp = FindByCondition(r => r.Id == employeeComponent.Id).FirstOrDefault();

            empComp.DateDeleted = DateTime.Now;

            Update(empComp);
            Save();
        }

        public IEnumerable<EmployeeComponent> GetAllEmployeeComponents()
        {
            return GetAllIncluded(r => r.Employee, r => r.Project, r => r.Component, r => r.CostCenter)
                   .Where(r => r.DateDeleted == null)
               .OrderByDescending(r => r.DateCreated);
        }

        public void UpdateEmployeeComponent(EmployeeComponent employeeComponent)
        {
            var empCopm = FindByCondition(r => r.Id == employeeComponent.Id).FirstOrDefault();

            empCopm.DateChange = DateTime.Now;

            empCopm.EmployeeId = employeeComponent.EmployeeId;
            empCopm.ComponentId = employeeComponent.ComponentId;
            empCopm.ProjectId = employeeComponent.ProjectId;
            empCopm.CostCenterId = employeeComponent.CostCenterId;
            empCopm.Days = employeeComponent.Days;
            empCopm.StartDate = employeeComponent.StartDate;
            empCopm.EndDate = employeeComponent.EndDate;
            empCopm.Scheme = employeeComponent.Scheme;
            empCopm.Amount = employeeComponent.Amount;
            empCopm.Currency = employeeComponent.Currency;
            empCopm.PaidByCash = employeeComponent.PaidByCash;
            empCopm.CashAmount = employeeComponent.CashAmount;
            empCopm.PaidMultiple = employeeComponent.PaidMultiple;

            Update(empCopm);
            Save();
        }
    }
    }
