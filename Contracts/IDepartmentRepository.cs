using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        IEnumerable<Department> GetAllDepartments();
        void CreateDepartment(Department department);
    }
}
