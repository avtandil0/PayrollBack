using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class CoefficientDTO
    {
        public Guid Id { get; set; }
        public double Gross { get; set; }
        public double Net { get; set; }
        public double Paid { get; set; }
        public double IncomeTax { get; set; }
        public double Pension { get; set; }
        public double Tax1 { get; set; }
        public double Tax2 { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
