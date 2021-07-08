using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICoefficientGroupRepository : IRepositoryBase<CoefficientGroup>
    {
        IEnumerable<CoefficientGroup> GetAllCoefficientGroup();
        void CreateCoefficientGroup(CoefficientGroup coefficientGroup);
    }
}
