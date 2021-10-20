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
            //return FindByCondition(r => r.DateDeleted == null)
            //    .OrderByDescending(ow => ow.DateCreated);

            return GetAllIncluded(r => r.Employees.Where(r => r.DateDeleted == null))
                   .Where(r => r.DateDeleted == null)
               .OrderByDescending(r => r.DateCreated);
        }

        public void CreateDepartment(Department department)
        {
            department.Id = Guid.NewGuid();
            department.DateCreated = DateTime.Now;
            Create(department);
            Save();
        }

        public void UpdateDepartment(Department department)
        {
            var dep = FindByCondition(r=> r.Id == department.Id).FirstOrDefault();

            dep.Name = department.Name;
            dep.DateChange = DateTime.Now;
            Update(dep);
            Save();
        }

        public void DeleteDepartment(Department department)
        {
            var dep = FindByCondition(r => r.Id == department.Id).FirstOrDefault();

            dep.DateDeleted = DateTime.Now;
            Update(dep);
            Save();
        }

       
    }
}
