using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class EmployeeGraceType
    {
        public EmployeeGraceType()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }
        public decimal? Amount { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
