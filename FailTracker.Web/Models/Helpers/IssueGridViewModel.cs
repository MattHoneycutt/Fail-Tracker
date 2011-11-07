using System;
using System.Collections.Generic;
using FailTracker.Web.Models.Shared;

namespace FailTracker.Web.Models.Helpers
{
	public class IssueGridViewModel
	{
		public IEnumerable<IssueSummaryViewModel> Issues { get; private set; }

		public bool AllowReordering { get; set; }

		public Guid ProjectID { get; set; }

		public IssueGridViewModel(Guid projectID, IEnumerable<IssueSummaryViewModel> issues)
		{
			ProjectID = projectID;
			Issues = issues;
		}
	}
}