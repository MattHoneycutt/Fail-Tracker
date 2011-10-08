using System;
using System.Linq;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;
using FailTracker.Web.Models.Shared;

namespace FailTracker.Web.Models.Backlog
{
	public class CompletedStoriesViewModel : IHaveCustomMappings
	{
		public IssueSummaryViewModel[] Issues { get; set; }

		public string ProjectName { get; set; }

		public Guid ProjectID { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Project, CompletedStoriesViewModel>()
				.ForMember(m => m.Issues, opt => opt.MapFrom(p => p.CurrentIssues.Where(i => i.Status == Status.Complete)))
				.ForMember(m => m.ProjectName, opt => opt.MapFrom(p => p.Name))
				.ForMember(m => m.ProjectID, opt => opt.MapFrom(p => p.ID));
		}
	}
}