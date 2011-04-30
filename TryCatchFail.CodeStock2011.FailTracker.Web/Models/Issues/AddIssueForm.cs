using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues
{
	public class AddIssueForm
	{
		[Required]
		public string Title { get; set; }
		[Required]
		[EmailAddress]
		public string AssignedTo { get; set; }
		[Required]
		[DataType(DataType.MultilineText)]
		public string Body { get; set; }
	}
}