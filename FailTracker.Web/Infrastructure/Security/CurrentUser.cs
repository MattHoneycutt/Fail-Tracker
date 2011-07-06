using System.Linq;
using System.Security.Principal;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Infrastructure.Security
{
	public class CurrentUser
	{
		private readonly IRepository<User> _users;
		private readonly IIdentity _currentUser;

		public CurrentUser(IRepository<User> users, IIdentity currentUser)
		{
			_users = users;
			_currentUser = currentUser;
		}

		public User Instance
		{
			get { return _users.Query().Single(u => u.EmailAddress == _currentUser.Name); }
		}
	}
}