using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class AccountsReportChart
    {
        public AccountsReportChart()
        {
            ComponentCreditAccounts = new HashSet<Component>();
            ComponentDebitAccounts = new HashSet<Component>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid? AccountsReportChartTypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual AccountsReportChartType AccountsReportChartType { get; set; }
        public virtual ICollection<Component> ComponentCreditAccounts { get; set; }
        public virtual ICollection<Component> ComponentDebitAccounts { get; set; }
    }
}
