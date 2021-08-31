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
            return FindByCondition(r => r.DateDeleted == null)
                .OrderByDescending(r => r.DateCreated);
        }

        public void CreateProject(Project project)
        {
            project.Id = Guid.NewGuid();
            project.DateCreated = DateTime.Now;
            Create(project);
            Save();
        }

        public void UpdateProject(Project project)
        {
            var pr = FindByCondition(r => r.Id == project.Id).FirstOrDefault();

            pr.DateChange = DateTime.Now;
            pr.Code = project.Code;
            pr.Description = project.Description;

            Update(pr);
            Save();
        }

        public void DeleteProject(Project project)
        {
            var pr = FindByCondition(r => r.Id == project.Id).FirstOrDefault();

            pr.DateDeleted = DateTime.Now;

            Update(pr);
            Save();
        }


    }
}
