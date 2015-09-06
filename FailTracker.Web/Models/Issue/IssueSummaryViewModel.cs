using System;
using FailTracker.Web.Domain;
using Heroic.AutoMapper;

namespace FailTracker.Web.Models.Issue
{
	public class IssueSummaryViewModel : IMapFrom<Domain.Issue>
	{
		public int IssueID { get; set; }
		public string Subject { get; set; }
		public DateTime CreatedAt { get; set; }
		public string CreatorUserName { get; set; }
		public IssueType IssueType { get; set; }
		public string AssignedToUserName { get; set; }
	}
}