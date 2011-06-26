using System;
using System.ComponentModel.DataAnnotations;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web.Models.Dashboard
{
	public class IssueSummaryViewModel : IMapFrom<Issue>
	{
		public Guid ID { get; set; }

		public string Title { get; set; }

		[DataType("User")]
		public string AssignedTo { get; set; }

		public IssueType Type { get; set; }

		public PointSize Size { get; set; }
	}
}