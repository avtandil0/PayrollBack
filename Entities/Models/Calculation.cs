using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Calculation
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? EmployeeComponentId { get; set; }
        public int? ResId { get; set; }
        public string CompCode { get; set; }
        public int? PayrollYear { get; set; }
        public int? PayrollMonth { get; set; }
        public DateTime? CalculationDate { get; set; }
        public int? SchemeTypeId { get; set; }
        public decimal Gross { get; set; }
        public decimal Net { get; set; }
        public decimal Paid { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal PensionTax { get; set; }
        public double Tax1 { get; set; }
        public double Tax2 { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }
        public decimal? RemainingGraceAmount { get; set; }
        public decimal? TotalBalance { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual EmployeeComponent EmployeeComponent { get; set; }
        public virtual SchemeType SchemeType { get; set; }
    }
}
