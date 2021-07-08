using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CoefficientRepository : RepositoryBase<Coefficient>, ICoefficientRepository
    {
        public CoefficientRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Coefficient> GetAllCoefficients()
        {
            return FindAll()
                .OrderByDescending(ow => ow.DateCreated);
        }

        public void CreateCoefficient(Coefficient coefficient)
        {
            coefficient.Id = Guid.NewGuid();
            coefficient.DateCreated = DateTime.Now;
            Create(coefficient);
            Save();
        }

    }
}
