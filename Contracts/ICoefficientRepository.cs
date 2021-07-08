using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{

    public interface ICoefficientRepository : IRepositoryBase<Coefficient>
    {
        IEnumerable<Coefficient> GetAllCoefficients();
        void CreateCoefficient(Coefficient coefficient);
    }
}
