using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return FindAll()
                .OrderByDescending(ow => ow.DateCreated);
        }

        public void CreateDepartment(Department department)
        {
            department.Id = Guid.NewGuid();
            department.DateCreated = DateTime.Now;
            Create(department);
            Save();
        }

    }
}
