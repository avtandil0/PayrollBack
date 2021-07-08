﻿using Contracts;
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
        private ICoefficientGroupRepository _coefficientGroup;

        public ICoefficientGroupRepository CoefficientGroup
        {
            get
            {
                if (_coefficientGroup == null)
                {
                    _coefficientGroup = new CoefficientGroupRepository(_repoContext);
                }

                return _coefficientGroup;
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
