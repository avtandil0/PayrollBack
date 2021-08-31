using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        IEnumerable<Project> GetAllProjects();
        void CreateProject(Project project);
        void UpdateProject(Project department);
        void DeleteProject(Project department);
    }
}
