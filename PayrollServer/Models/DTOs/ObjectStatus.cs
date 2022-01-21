using System.Collections.Generic;

namespace PayrollServer.Models.DTOs
{
    public class ObjectStatus
    {
        public List<string> FieldNames { get; set; }
        public int Value { get; set; } // Active = 1; Passive = 0
    }
}   