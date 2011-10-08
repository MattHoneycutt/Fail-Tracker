using System;

namespace FailTracker.Web.Models.Shared
{
	public class ProjectToolbarViewModel
	{
		public Guid ProjectID { get; private set; }

		public ProjectToolbarViewModel(Guid projectID)
		{
			ProjectID = projectID;
		}
	}
}