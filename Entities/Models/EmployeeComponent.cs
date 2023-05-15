using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class EmployeeComponent
    {
        public EmployeeComponent()
        {
            Calculations = new HashSet<Calculation>();
        }

        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ComponentId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? CostCenterId { get; set; }
        public int? PaymentDaysTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SchemeTypeId { get; set; }
        public decimal Amount { get; set; }
        public int Currency { get; set; }
        public bool PaidByCash { get; set; }
        public decimal CashAmount { get; set; }
        public bool? PaidMultiple { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool? IsPermanent { get; set; }

        public virtual Component Component { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PaymentDaysType PaymentDaysType { get; set; }
        public virtual Project Project { get; set; }
        public virtual SchemeType SchemeType { get; set; }
        public virtual ICollection<Calculation> Calculations { get; set; }
    }
}
