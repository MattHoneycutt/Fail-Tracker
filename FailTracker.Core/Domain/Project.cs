using System;
using System.Collections.Generic;

namespace FailTracker.Core.Domain
{
	public class Project
	{
		public virtual Guid ID { get; set; }

		public virtual string Name { get; private set; }

		public virtual IEnumerable<Issue> CurrentIssues { get; private set; }

		protected Project()
		{
			CurrentIssues = new List<Issue>();
		}

		public static Project Create(string name)
		{
			return new Project {Name = name};
		}

		public virtual void AttachIssue(Issue issue)
		{
			((IList<Issue>) CurrentIssues).Add(issue);
		}
	}
}