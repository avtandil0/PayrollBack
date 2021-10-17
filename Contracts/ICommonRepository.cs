using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICommonRepository
    {
        IEnumerable<AccountsReportChartType> GetAccountsReportChartTypes();
        IEnumerable<SchemeType> GetSchemeTypes();
        IEnumerable<PaymentDaysType> GetPaymentDaysTypes();
        IEnumerable<EmployeeType> GetEmployeeTypes();
    }
}
