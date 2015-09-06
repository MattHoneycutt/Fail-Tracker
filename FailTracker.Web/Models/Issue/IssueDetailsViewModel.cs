using System;
using System.ComponentModel;
using FailTracker.Web.Domain;
using Heroic.AutoMapper;
using FailTracker.Web.Infrastructure.ModelMetadata;

namespace FailTracker.Web.Models.Issue
{
	public class IssueDetailsViewModel : IMapFrom<Domain.Issue>
	{
		[Render(ShowForEdit = false)]
		public int IssueID { get; set; }

		[Render(ShowForEdit = false)]
		public DateTime CreatedAt { get; set; }

		[ReadOnly(true)]
		public string CreatorUserName { get; set; }

		public string Subject { get; set; }

		public IssueType IssueType { get; set; }

		public string AssignedToUserName { get; set; }

		public string Body { get; set; }
	}
}