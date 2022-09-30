using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class TimeSheet
    {
        public Guid Id { get; set; }
        public int SheetId { get; set; }
        public string Name { get; set; }
        public string WeekDay { get; set; }
        public string WorkingStartTime { get; set; }
        public string WorkingEndTime { get; set; }
        public string BreakingStartTime { get; set; }
        public string BreakingEndTime { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
