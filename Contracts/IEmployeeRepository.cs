using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(Guid id);
        IEnumerable<Employee> GetEmployeeByDepartment(Guid depId);
        void CreateEmployee(Employee employee);
        void ImportEmployee(List<Humre> humres);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
