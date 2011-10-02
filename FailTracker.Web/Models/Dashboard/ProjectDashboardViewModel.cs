using System;
using System.Collections.Generic;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;
using System.Linq;

namespace FailTracker.Web.Models.Dashboard
{
	public class ProjectDashboardViewModel : IHaveCustomMappings
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public IEnumerable<IssueSummaryViewModel> ActiveIssues { get; set; }

		public int NumTotalActiveIssues { get; set; }

		public int NumTotalCompletedIssues { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Project, ProjectDashboardViewModel>()
				.ForMember(m => m.ActiveIssues, opt => opt.MapFrom(p => p.CurrentIssues.Where(i => i.Status != Status.Complete).OrderByDescending(i => i.LastChanged).Take(3)))
				.ForMember(m => m.NumTotalActiveIssues, opt => opt.MapFrom(p => p.CurrentIssues.Count(i => i.Status != Status.Complete)))
				.ForMember(m => m.NumTotalCompletedIssues, opt => opt.MapFrom(p => p.CurrentIssues.Count(i => i.Status == Status.Complete)));
		}
	}
}