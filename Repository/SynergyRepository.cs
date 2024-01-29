using Contracts;
using Entities;
using Entities.HelperModels;
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

            if (humre.ResId != null)
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

        public IEnumerable<HumreHelper> GetHumresByIds(List<int?> ids)
        {
            var res = from h in repository.Humres
                      join j in repository.Hrjbtls on h.JobTitle equals j.JobTitle
                      //join b in repository.Bnkkops on h.Crdnr equals b.Crdnr into leftbh
                      //from x in leftbh.DefaultIfEmpty()
                      where ids.Contains(h.ResId)
                      select new HumreHelper
                      {
                          FirstName = h.FirstName,
                          SurName = h.SurName,
                          Adres1 = h.Adres1,
                          LandIso = h.LandIso,
                          Position = j.Descr50,
                          TelnrPrv = h.TelnrPrv,
                          ResId = h.ResId,
                          //BankAccountNumber = x.BankRek
                          BankAccountNumber = ""
                      };
            return res;
        }
    }
}
