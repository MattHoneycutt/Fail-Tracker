using System;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues
{
	public class IssueViewModel
	{
		public Guid ID { get; set; }

		public string Title { get; set; }

		public string AssignedTo { get; set; }
	}
}