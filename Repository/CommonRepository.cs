﻿using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly RepositoryContext repository;

        public CommonRepository(RepositoryContext repositoryContext)
        {
            this.repository = repositoryContext;
        }

        public IEnumerable<AccountsReportChartType> GetAccountsReportChartTypes()
        {
            return repository.AccountsReportChartTypes.Where(r => r.DateDeleted == null).OrderByDescending(r => r.DateCreated);
        }
    }
}
