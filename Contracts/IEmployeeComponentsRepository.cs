using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEmployeeComponentsRepository : IRepositoryBase<EmployeeComponent>
    {
        IEnumerable<EmployeeComponent> GetAllEmployeeComponents();
        void CreateEmployeeComponent(EmployeeComponent employeeComponent);
        void UpdateEmployeeComponent(EmployeeComponent employeeComponent);
        void DeleteEmployeeComponent(EmployeeComponent employeeComponent);
    }
}
