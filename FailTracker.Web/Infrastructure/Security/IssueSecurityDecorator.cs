using System.Linq;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Infrastructure.Security
{
	public class IssueSecurityDecorator : IRepository<Issue>
	{
		private readonly IRepository<Issue> _repository;
		private readonly CurrentUser _user;
		
		public IssueSecurityDecorator(IRepository<Issue> repository, CurrentUser user)
		{
			_repository = repository;
			_user = user;
		}

		public void Save(Issue entity)
		{
			_repository.Save(entity);
		}

		public IQueryable<Issue> Query()
		{
			//TODO: Eventually Fail Tracker admins should be able to see everything. 
			return from i in _repository.Query()
			       where i.Project.Members.Contains(_user.Instance)
			       select i;
		}
	}
}