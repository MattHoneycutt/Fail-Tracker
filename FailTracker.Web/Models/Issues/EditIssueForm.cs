using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Models.Issues
{
	public class EditIssueForm
	{
		[HiddenInput(DisplayValue = false)]
		public Guid ID { get; set; }

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
		public string Description { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Comments { get; set; }
	}
}