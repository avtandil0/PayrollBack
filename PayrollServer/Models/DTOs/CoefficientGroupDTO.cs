using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class CoefficientGroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid? PensionCoefficientId { get; set; }
        public Guid? StandartCoefficientId { get; set; }
    }
}
