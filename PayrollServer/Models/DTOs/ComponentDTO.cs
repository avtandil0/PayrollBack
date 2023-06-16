using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class ComponentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CreditAccountId { get; set; }
        public string CreditAccountName { get; set; }
        public string Code { get; set; }
        public Guid? DebitAccountId { get; set; }
        public string DebitAccountName { get; set; }
        public Guid? CoefficientId { get; set; }
        public string CoefficientName { get; set; }
        public ObjectStatus Status { get; set; }
        public int Type { get; set; } // 1 - დარიცხვა, 2 დაკავება
        public bool IgnoreIncome { get; set; } 
        public string TypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
    
}
