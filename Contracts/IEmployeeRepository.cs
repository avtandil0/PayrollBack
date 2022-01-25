using Entities.FilterModels;
using Entities.HelperModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(Guid id);
        IEnumerable<Employee> GetEmployeeByDepartment(Guid depId);
        IEnumerable<Employee> GetCalculationByFilter(CalculationFilter calculationFilter);
        Guid CreateEmployee(Employee employee);
        void ImportEmployee(List<HumreHelper> humres);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
