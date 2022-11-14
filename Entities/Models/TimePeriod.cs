using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class TimePeriod
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool? IsBreakTime { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
