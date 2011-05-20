using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Models.Issues
{
	public class ViewIssueViewModel
	{
		[UIHint("Title")]
		public string Title { get; set; }

		[DataType("User")]
		public string CreatedBy { get; set; }

		[DataType("User")]
		[DisplayName("Assigned To")]
		public string AssignedTo { get; set; }

		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		public PointSize Size { get; set; }

		public IssueType Type { get; set; }
	}
}