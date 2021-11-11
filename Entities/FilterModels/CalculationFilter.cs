using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.FilterModels
{
    public class CalculationFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? DepartmentId { get; set; }
        public DateTime? CalculationPeriod { get; set; }
    }
}
