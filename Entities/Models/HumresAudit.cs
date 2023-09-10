using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class HumresAudit
    {
        public int Id { get; set; }
        public int ResId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
