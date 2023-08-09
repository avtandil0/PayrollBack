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
        public bool Calculated { get; set; }
        public List<Guid> DepartmentId { get; set; }
        public List<string> ComponentId { get; set; }
        public List<Guid> NotIncludes { get; set; }
        public DateTime? CalculationPeriod { get; set; }
    }
}
