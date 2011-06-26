using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.ModelMetadata;

namespace FailTracker.Web.Models.Issues
{
	public class AddIssueForm
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public IssueType Type { get; set; }

		[Required]
		public PointSize Size { get; set; }

		[DisplayName("Assigned To")]
		[UIHint("UserDropDown")]
		public Guid? AssignedTo { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		[RenderMode(RenderMode.None)]
		public User CurrentUser { get; set; }
	}
}