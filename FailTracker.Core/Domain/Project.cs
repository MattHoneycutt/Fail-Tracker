using System;
using System.Collections.Generic;

namespace FailTracker.Core.Domain
{
	public class Project
	{
		public virtual Guid ID { get; set; }

		public virtual string Name { get; private set; }

		//TODO: At some point, we need to rework this property so that it only returns 
		//		the issues that are not completed. 
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

		public virtual void MoveIssue(Issue toMove, Issue target)
		{
			var issues = ((IList<Issue>) CurrentIssues);

			if (!issues.Contains(toMove))
			{
				throw new InvalidOperationException("The issue you are attempting to move does not belong to the project.  Attach it to the project first, then try moving it.");
			}

			var index = issues.IndexOf(target);

			if (index < 0)
			{
				throw new InvalidOperationException("The target issue does not belong to the project.  Attach it to the project first, then try moving an issue before it.");
			}

			issues.Remove(toMove);

			issues.Insert(index, toMove);
		}
	}
}