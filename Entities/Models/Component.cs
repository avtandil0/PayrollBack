using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Component
    {
        public Component()
        {
            EmployeeComponents = new HashSet<EmployeeComponent>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CreditAccountId { get; set; }
        public Guid? DebitAccountId { get; set; }
        public Guid? CoefficientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Coefficient Coefficient { get; set; }
        public virtual AccountsReportChart CreditAccount { get; set; }
        public virtual AccountsReportChart DebitAccount { get; set; }
        public virtual ICollection<EmployeeComponent> EmployeeComponents { get; set; }
    }
}
