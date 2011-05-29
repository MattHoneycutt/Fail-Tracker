using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web.Models.Issues
{
	public class IssueDetailsViewModel : IMapFrom<Issue>
	{
		public Guid ID { get; set; }

		[UIHint("Title")]
		public string Title { get; set; }

		[DataType("User")]
		public string CreatedBy { get; set; }

		public DateTime CreatedAt { get; set; }

		[DataType("User")]
		[DisplayName("Assigned To")]
		public string AssignedToEmailAddress { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public PointSize Size { get; set; }

		public IssueType Type { get; set; }

		public ChangeViewModel[] Changes { get; set; }
	}
}