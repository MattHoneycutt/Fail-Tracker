using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web.Models.Issues
{
	//TODO: Can we use a marker interface for basic/standard mapping, and another
	//		for more advanced mapping? 

	public class IssueDetailsViewModel : IMappable
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
		
		void IMappable.CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Issue, IssueDetailsViewModel>();
		}
	}
}