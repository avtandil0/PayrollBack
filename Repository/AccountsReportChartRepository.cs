using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class AccountsReportChartRepository : RepositoryBase<AccountsReportChart>, IAccountsReportChartRepository
    {
        public AccountsReportChartRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<AccountsReportChart> GetAllAccountsReportChart()
        {
            return FindAll()
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateAccountsReportChart(AccountsReportChart accountsReportChart)
        {
            accountsReportChart.Id = Guid.NewGuid();
            accountsReportChart.DateCreated = DateTime.Now;
            Create(accountsReportChart);
            Save();
        }

    }
}
