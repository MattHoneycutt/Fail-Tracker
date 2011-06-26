using System.Collections.Generic;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web.Models.Dashboard
{
	public class ProjectDashboardViewModel : IMapFrom<Project>
	{
		public string Name { get; set; }

		public IEnumerable<IssueSummaryViewModel> CurrentIssues { get; set; }
	}
}