using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FailTracker.Web.Domain;
using FailTracker.Web.Filters;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web.Models.Issue
{
	public class EditIssueForm : IMapFrom<Domain.Issue>
	{
		[HiddenInput]
		public int IssueID { get; set; }

		[ReadOnly(true)]
		public string CreatorUserName { get; set; }

		[Required]
		public string Subject { get; set; }

		public IssueType IssueType { get; set; }
	
		[Display(Name = "Assigned To")]
		public string AssignedToUserName { get; set; }
		
		[Required]
		public string Body { get; set; }
	}
}