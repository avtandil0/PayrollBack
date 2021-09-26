using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class EmployeeComponentDTO
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ComponentId { get; set; }
        public string ComponentName { get; set; }
        public Guid? ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public Guid? CostCenterId { get; set; }
        public string CostCenterCode { get; set; }
        public int PaymentDaysTypeId { get; set; }
        public string PaymentDaysTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SchemeTypeId { get; set; }
        public string SchemeTypeName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public bool PaidByCash { get; set; }
        public decimal CashAmount { get; set; }
        public bool? PaidMultiple { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
