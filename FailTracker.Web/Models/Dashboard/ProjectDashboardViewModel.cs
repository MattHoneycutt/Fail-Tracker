using System;
using System.Collections.Generic;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web.Models.Dashboard
{
	public class ProjectDashboardViewModel : IMapFrom<Project>
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public IEnumerable<IssueSummaryViewModel> CurrentIssues { get; set; }
	}
}