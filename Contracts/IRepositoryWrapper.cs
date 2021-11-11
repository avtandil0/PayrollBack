using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IDepartmentRepository Department { get; }
        ICoefficientRepository Coefficient { get; }
        IProjectRepository Project { get; }
        ICostCenterRepository CostCenter { get; }
        IAccountsReportChartRepository AccountsReportChart { get; }
        //ICoefficientGroupRepository CoefficientGroup { get; }
        ICommonRepository Common { get; }
        IComponentRepository Component { get; }
        IEmployeeRepository Employee { get; }
        IEmployeeComponentsRepository EmployeeComponents { get; }
        ICalculationRepository Calculation { get; }
    }
}
