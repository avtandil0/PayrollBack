using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICostCenterRepository : IRepositoryBase<CostCenter>
    {
        IEnumerable<CostCenter> GetAllCostCenter();
        void CreateCostCenter(CostCenter costCenter);
        void UpdateCostCenter(CostCenter costCenter);
        void DeleteCostCenter(CostCenter costCenter);
    }
}
