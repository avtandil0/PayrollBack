using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class EmployeeCalculationDTO
    {
        public Guid Id { get; set; }
        public int? ResId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PayrollYear { get; set; }
        public int? PayrollMonth { get; set; }
        public DateTime? CalculationDate { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Income { get; set; }
        public decimal? Pension { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? ToBePaid { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Debit { get; set; }
        public decimal? ToTalBalance { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }


    }
}
