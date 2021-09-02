using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models.DTOs
{
    public class AccountsReportChartDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid? AccountsReportChartTypeId { get; set; }
        public string AccountsReportChartTypeName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
