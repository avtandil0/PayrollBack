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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CalculationPeriod { get; set; }
        public DateTime CalculationDate { get; set; }
        public double Gross { get; set; }
        public double Amount { get; set; }
        public double Income { get; set; }
        public double Pension { get; set; }
        public double Deduction { get; set; }
        public double ToBePaid { get; set; }
        public double Paid { get; set; }
        public double Debit { get; set; }
        public double ToTalBalance { get; set; }
    }
}
