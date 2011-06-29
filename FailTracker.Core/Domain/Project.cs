using System;
using System.Collections.Generic;

namespace FailTracker.Core.Domain
{
	public class Project
	{
		public virtual Guid ID { get; set; }

		public virtual string Name { get; private set; }

		public virtual IEnumerable<Issue> CurrentIssues { get; private set; }

		public virtual User Creator { get; private set; }

		public virtual IEnumerable<User> Members { get; private set; }

		protected Project()
		{
			CurrentIssues = new List<Issue>();
			Members = new List<User>();
		}

		public static Project Create(string name, User creator)
		{
			var project = new Project {Name = name, Creator = creator};

			project.AddMember(creator);

			return project;
		}

		public virtual void AddMember(User user)
		{
			((IList<User>) Members).Add(user);
		}

		public virtual void AttachIssue(Issue issue)
		{
			((IList<Issue>) CurrentIssues).Add(issue);
		}
	}
}