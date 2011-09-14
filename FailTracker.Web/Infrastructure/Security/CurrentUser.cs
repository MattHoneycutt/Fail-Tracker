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
		private User _user;

		public CurrentUser(IRepository<User> users, IIdentity currentUser)
		{
			_users = users;
			_currentUser = currentUser;
		}

		internal CurrentUser(User user)
		{
			_user = user;
		}

		public User Instance
		{
			get { return _user ?? (_user = _users.Query().Single(u => u.EmailAddress == _currentUser.Name)); }
		}
	}
}