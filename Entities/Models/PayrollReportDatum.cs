using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class PayrollReportDatum
    {
        public Guid Id { get; set; }
        public int HrcompTransId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public int ResId { get; set; }
        public int PayrollYear { get; set; }
        public int Period { get; set; }
        public DateTime CalculationDate { get; set; }
        public string CompCode { get; set; }
        public int? PensionSchema { get; set; }
        public double BaseValue { get; set; }
        public double IssuedAmount { get; set; }
        public double GraceValue { get; set; }
        public double IncomeTax { get; set; }
        public double InitialGrace { get; set; }
        public double RemainingGrace { get; set; }
        public string LandIso { get; set; }
        public string LandIsonr { get; set; }
        public string Oms600 { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
