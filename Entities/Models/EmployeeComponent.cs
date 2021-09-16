using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class EmployeeComponent
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ComponentId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? CostCenterId { get; set; }
        public string Days { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Scheme { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public bool PaidByCash { get; set; }
        public decimal CashAmount { get; set; }
        public bool? PaidMultiple { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Component Component { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}
