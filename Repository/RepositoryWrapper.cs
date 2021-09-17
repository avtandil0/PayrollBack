using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IDepartmentRepository _department;
        private ICoefficientRepository _coefficient;
        private IProjectRepository _project;
        private ICostCenterRepository _costCenter;
        private IAccountsReportChartRepository _accountsReportChart;
        private ICommonRepository _common;
        private IComponentRepository _component;
        private IEmployeeRepository _employee;
        private IEmployeeComponentsRepository _employeeComponents;


        public IEmployeeComponentsRepository EmployeeComponents
        {
            get
            {
                if (_employeeComponents == null)
                {
                    _employeeComponents = new EmployeeComponentsRepository(_repoContext);
                }

                return _employeeComponents;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = new EmployeeRepository(_repoContext, _employeeComponents);
                }

                return _employee;
            }
        }

        public IComponentRepository Component
        {
            get
            {
                if (_component == null)
                {
                    _component = new ComponentRepository(_repoContext);
                }

                return _component;
            }
        }


        public ICommonRepository Common
        {
            get
            {
                if (_common == null)
                {
                    _common = new CommonRepository(_repoContext);
                }

                return _common;
            }
        }

        public ICoefficientRepository Coefficient
        {
            get
            {
                if (_coefficient == null)
                {
                    _coefficient = new CoefficientRepository(_repoContext);
                }

                return _coefficient;
            }
        }

        public IAccountsReportChartRepository AccountsReportChart
        {
            get
            {
                if (_accountsReportChart == null)
                {
                    _accountsReportChart = new AccountsReportChartRepository(_repoContext);
                }

                return _accountsReportChart;
            }
        }

        public ICostCenterRepository CostCenter
        {
            get
            {
                if (_costCenter == null)
                {
                    _costCenter = new CostCenterRepository(_repoContext);
                }

                return _costCenter;
            }
        }

        public IProjectRepository Project
        {
            get
            {
                if (_project == null)
                {
                    _project = new ProjectRepository(_repoContext);
                }

                return _project;
            }
        }


        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentRepository(_repoContext);
                }

                return _department;
            }
        }

        

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}
