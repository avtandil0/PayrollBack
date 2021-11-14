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
            return FindByCondition(r => r.DateDeleted == null)
                .OrderByDescending(ow => ow.DateCreated);
        }

        public void CreateCoefficient(Coefficient coefficient)
        {
            coefficient.Id = Guid.NewGuid();
            coefficient.DateCreated = DateTime.Now;
            Create(coefficient);
            Save();
        }

        public void UpdateCoefficient(Coefficient coefficient)
        {
            var coef = FindByCondition(r => r.Id == coefficient.Id).FirstOrDefault();

            coef.DateChange = DateTime.Now;

            coef.Description = coefficient.Description;
            coef.Name = coefficient.Name;

            coef.Sgross = coefficient.Sgross;
            coef.Snet = coefficient.Snet;
            coef.Spaid = coefficient.Spaid;
            coef.Spension = coefficient.Spension;
            coef.SincomeTax = coefficient.SincomeTax;
            coef.Stax1 = coefficient.Stax1;
            coef.Stax2 = coefficient.Stax2;

            coef.Pgross = coefficient.Pgross;
            coef.Pnet = coefficient.Pnet;
            coef.Ppaid = coefficient.Ppaid;
            coef.Ppension = coefficient.Ppension;
            coef.PincomeTax = coefficient.PincomeTax;
            coef.Ptax1 = coefficient.Ptax1;
            coef.Ptax2 = coefficient.Ptax2;


            Update(coef);
            Save();
        }

        public void DeleteCoefficient(Coefficient coefficient)
        {
            var coef = FindByCondition(r => r.Id == coefficient.Id).FirstOrDefault();

            coef.DateDeleted = DateTime.Now;
            Update(coef);
            Save();
        }
    }
}
