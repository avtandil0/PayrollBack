using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class CalculationDTO
    {
        public Guid Id { get; set; }
        public int? ResId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CalculationPeriod { get; set; }
        public DateTime CalculationDate { get; set; }
        public int? PayrollYear { get; set; }
        public int? PayrollMonth { get; set; }
        public decimal Gross { get; set; }
        public decimal Net { get; set; }
        public decimal Paid { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal PensionTax { get; set; }
        public double Tax1 { get; set; }
        public double Tax2 { get; set; }
        public decimal? RemainingGraceAmount { get; set; }
        public decimal? TotalBalance { get; set; }
        public virtual EmployeeComponentDTO EmployeeComponent { get; set; }
    }
}
