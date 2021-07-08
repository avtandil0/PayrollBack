using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return FindAll()
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateProject(Project project)
        {
            project.Id = Guid.NewGuid();
            project.DateCreated = DateTime.Now;
            Create(project);
            Save();
        }

    }
}
