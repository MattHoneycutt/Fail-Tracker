using System;
using System.ComponentModel.DataAnnotations;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Models.Issues
{
	public class IssueViewModel
	{
		public Guid ID { get; set; }

		public string Title { get; set; }

		[DataType("User")]
		public string AssignedTo { get; set; }

		public IssueType Type { get; set; }

		public PointSize Size { get; set; }
	}
}