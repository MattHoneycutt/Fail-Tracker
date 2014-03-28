using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FailTracker.Web.Domain;
using FailTracker.Web.Filters;

namespace FailTracker.Web.Models.Issue
{
	public class NewIssueForm
	{
		[Required]
		public string Subject { get; set; }
		
		[Required]
		public IssueType IssueType { get; set; }

		[Required, Display(Name = "Assigned To")]
		public string AssignedToUserID { get; set; }

		[Required]
		public string Body { get; set; }
	}
}