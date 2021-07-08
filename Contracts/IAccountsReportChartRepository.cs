using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAccountsReportChartRepository : IRepositoryBase<AccountsReportChart>
    {
        IEnumerable<AccountsReportChart> GetAllAccountsReportChart();
        void CreateAccountsReportChart(AccountsReportChart costCenter);
    }
}
