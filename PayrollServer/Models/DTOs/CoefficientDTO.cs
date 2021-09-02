using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class CoefficientDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Sgross { get; set; }
        public double Snet { get; set; }
        public double Spaid { get; set; }
        public double SincomeTax { get; set; }
        public double Spension { get; set; }
        public double Stax1 { get; set; }
        public double Stax2 { get; set; }
        public double Pgross { get; set; }
        public double Pnet { get; set; }
        public double Ppaid { get; set; }
        public double PincomeTax { get; set; }
        public double Ppension { get; set; }
        public double Ptax1 { get; set; }
        public double Ptax2 { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
