using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IComponentRepository : IRepositoryBase<Component>
    {
        IEnumerable<Component> GetAllComponents();
        IEnumerable<Component> GetAllActiveComponents();
        void CreateComponent(Component department);
        void UpdateComponent(Component department);
        void DeleteComponent(Component department);
    }
}
