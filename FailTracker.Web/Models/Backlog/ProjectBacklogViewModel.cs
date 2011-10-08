using System;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;
using FailTracker.Web.Models.Shared;

namespace FailTracker.Web.Models.Backlog
{
	public class ProjectBacklogViewModel : IHaveCustomMappings
	{
		public IssueSummaryViewModel[] Issues { get; set; }

		public string ProjectName { get; set; }

		public Guid ProjectID { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Project, ProjectBacklogViewModel>()
				.ForMember(m => m.Issues, opt => opt.MapFrom(p => p.CurrentIssues))
				.ForMember(m => m.ProjectName, opt => opt.MapFrom(p => p.Name))
				.ForMember(m => m.ProjectID, opt => opt.MapFrom(p => p.ID));
		}
	}
}