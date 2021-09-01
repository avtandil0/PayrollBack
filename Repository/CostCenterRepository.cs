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
            return FindByCondition(r => r.DateDeleted == null)
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateCostCenter(CostCenter costCenter)
        {
            costCenter.Id = Guid.NewGuid();
            costCenter.DateCreated = DateTime.Now;
            Create(costCenter);
            Save();
        }

        public void UpdateCostCenter(CostCenter costCenter)
        {
            var cs = FindByCondition(r => r.Id == costCenter.Id).FirstOrDefault();

            cs.DateChange = DateTime.Now;
            cs.Description = costCenter.Description;
            cs.Code = costCenter.Code;

            Update(cs);
            Save();
        }

        public void DeleteCostCenter(CostCenter costCenter)
        {
            var cs = FindByCondition(r => r.Id == costCenter.Id).FirstOrDefault();

            cs.DateDeleted = DateTime.Now;

            Update(cs);
            Save();
        }

    }
}
