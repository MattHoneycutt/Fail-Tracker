using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;
using System.Linq;

namespace FailTracker.Web.Models.ProjectAdministration
{
	public class ProjectSummaryViewModel : IHaveCustomMappings
	{
		public string Name { get; set; }

		public int ActiveStories { get; set; }

		void IHaveCustomMappings.CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Project, ProjectSummaryViewModel>()
				.ForMember(m => m.ActiveStories, opt => opt.MapFrom(p => p.CurrentIssues.Count()));
		}
	}
}