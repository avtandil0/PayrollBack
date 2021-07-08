using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CostCenterRepository : RepositoryBase<CostCenter>, ICostCenterRepository
    {
        public CostCenterRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<CostCenter> GetAllCostCenter()
        {
            return FindAll()
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateCostCenter(CostCenter costCenter)
        {
            costCenter.Id = Guid.NewGuid();
            costCenter.DateCreated = DateTime.Now;
            Create(costCenter);
            Save();
        }

    }
}
