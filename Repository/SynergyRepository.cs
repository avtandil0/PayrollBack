using Contracts;
using Entities;
using Entities.Models;
using PayrollServer.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class SynergyRepository : ISynergyRepository
    {
        private readonly SynergyContext repository;

        public SynergyRepository(SynergyContext repositoryContext)
        {
            this.repository = repositoryContext;
        }
        public IEnumerable<Humre> GetHumres()
        {
            return repository.Humres.Where(r => r.ResId > 0);
        }

        public IEnumerable<Humre> GetHumresByFilter(HumreFilter humre)
        {
            var query = repository.Humres.Where(r => r.ResId > 0);

            if(humre.ResId != null)
            {
                query = query.Where(r => r.ResId == humre.ResId);
            }

            if (!string.IsNullOrEmpty(humre.FirstName))
            {
                query = query.Where(r => r.FirstName.Contains(humre.FirstName));
            }

            if (!string.IsNullOrEmpty(humre.SurName))
            {
                query = query.Where(r => r.SurName.Contains(humre.SurName));
            }

            return query;
        }

        public IEnumerable<Humre> GetHumresByIds(List<int> ids)
        {
            return repository.Humres.Where(r => ids.Contains(r.Id));
        }
    }
}
