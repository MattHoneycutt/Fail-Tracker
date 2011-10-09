using System;

namespace FailTracker.Web.Models.Helpers
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