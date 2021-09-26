using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class SchemeType
    {
        public SchemeType()
        {
            EmployeeComponents = new HashSet<EmployeeComponent>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<EmployeeComponent> EmployeeComponents { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
