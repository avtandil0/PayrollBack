using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ComponentRepository : RepositoryBase<Component>, IComponentRepository
    {
        public ComponentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Component> GetAllComponents()
        {
            return GetAllIncluded(r => r.CreditAccount, r => r.DebitAccount, r => r.Coefficient)
                    .Where(r => r.DateDeleted == null)
                .OrderByDescending(r => r.DateCreated);
        }

        public IEnumerable<Component> GetAllActiveComponents()
        {
            var current = DateTime.Now;

            return GetAllIncluded(r => r.CreditAccount, r => r.DebitAccount, r => r.Coefficient)
                    .Where(r => r.DateDeleted == null && r.StartDate.Date <= current.Date
                                                      && r.EndDate.Date >= current.Date)
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateComponent(Component component)
        {
            component.Id = Guid.NewGuid();
            component.DateCreated = DateTime.Now;
            Create(component);
            Save();
        }

        public void UpdateComponent(Component component)
        {
            var com = FindByCondition(r=> r.Id == component.Id).FirstOrDefault();

            com.DateChange = DateTime.Now;
            com.Name = component.Name;
            com.CoefficientId = component.CoefficientId;
            com.CreditAccountId = component.CreditAccountId;
            com.DebitAccountId = component.DebitAccountId;
            com.StartDate = component.StartDate;
            com.EndDate = component.EndDate;
            com.Code = component.Code;
            com.Type = component.Type;

            Update(com);
            Save();
        }

        public void DeleteComponent(Component component)
        {
            var com = FindByCondition(r => r.Id == component.Id).FirstOrDefault();

            com.DateDeleted = DateTime.Now;

            Update(com);
            Save();
        }

    }
}
