using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Coefficient
    {
        public Coefficient()
        {
            CoefficientGroupPensionCoefficients = new HashSet<CoefficientGroup>();
            CoefficientGroupStandartCoefficients = new HashSet<CoefficientGroup>();
        }

        public Guid Id { get; set; }
        public double Gross { get; set; }
        public double Net { get; set; }
        public double Paid { get; set; }
        public double IncomeTax { get; set; }
        public double Pension { get; set; }
        public double Tax1 { get; set; }
        public double Tax2 { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<CoefficientGroup> CoefficientGroupPensionCoefficients { get; set; }
        public virtual ICollection<CoefficientGroup> CoefficientGroupStandartCoefficients { get; set; }
    }
}
