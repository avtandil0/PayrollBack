using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.FilterModels
{
    public class CalculationFilter
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Guid> DepartmentId { get; set; }
        public Guid ComponentId { get; set; }
        public DateTime? CalculationPeriod { get; set; }
    }
}
