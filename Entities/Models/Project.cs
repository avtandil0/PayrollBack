using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Project
    {
        public Project()
        {
            EmployeeComponents = new HashSet<EmployeeComponent>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<EmployeeComponent> EmployeeComponents { get; set; }
    }
}
