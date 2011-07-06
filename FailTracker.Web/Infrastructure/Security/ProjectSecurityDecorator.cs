using System.Linq;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Infrastructure.Security
{
	public class ProjectSecurityDecorator : IRepository<Project>
	{
		private readonly IRepository<Project> _repository;
		private readonly CurrentUser _user;

		public ProjectSecurityDecorator(IRepository<Project> repository, CurrentUser user)
		{
			_repository = repository;
			_user = user;
		}

		public void Save(Project entity)
		{
			_repository.Save(entity);
		}

		public IQueryable<Project> Query()
		{
			//TODO: Eventually Fail Tracker admins should be able to see everything. 
			return from p in _repository.Query()
			       where p.Members.Contains(_user.Instance)
			       select p;
		}
	}
}