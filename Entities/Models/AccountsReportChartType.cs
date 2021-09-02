using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class AccountsReportChartType
    {
        public AccountsReportChartType()
        {
            AccountsReportCharts = new HashSet<AccountsReportChart>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChange { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<AccountsReportChart> AccountsReportCharts { get; set; }
    }
}
