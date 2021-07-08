using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CoefficientGroupRepository : RepositoryBase<CoefficientGroup>, ICoefficientGroupRepository
    {
        public CoefficientGroupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<CoefficientGroup> GetAllCoefficientGroup()
        {
            return FindAll()
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateCoefficientGroup(CoefficientGroup coefficientGroup)
        {
            coefficientGroup.Id = Guid.NewGuid();
            coefficientGroup.DateCreated = DateTime.Now;
            Create(coefficientGroup);
            Save();
        }

    }
}
