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
            return GetAllIncluded(r => r.AccountsReportChartType)
                    .Where(r => r.DateDeleted == null)
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateAccountsReportChart(AccountsReportChart accountsReportChart)
        {
            accountsReportChart.Id = Guid.NewGuid();
            accountsReportChart.DateCreated = DateTime.Now;
            Create(accountsReportChart);
            Save();
        }

        public void UpdateAccountsReportChart(AccountsReportChart accountsReportChart)
        {
            var acc = FindByCondition(r => r.Id == accountsReportChart.Id).FirstOrDefault();

            acc.DateChange = DateTime.Now;
            acc.Description = accountsReportChart.Description;
            acc.AccountsReportChartTypeId = accountsReportChart.AccountsReportChartTypeId;

            Update(acc);
            Save();
        }

        public void DeleteAccountsReportChart(AccountsReportChart accountsReportChart)
        {
            var acc = FindByCondition(r => r.Id == accountsReportChart.Id).FirstOrDefault();

            acc.DateDeleted = DateTime.Now;

            Update(acc);
            Save();
        }

    }
}
