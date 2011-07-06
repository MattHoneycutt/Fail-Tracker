using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.ModelMetadata;
using FailTracker.Web.Infrastructure.ModelMetadata.Attributes;

namespace FailTracker.Web.Models.ProjectAdministration
{
	public class AddProjectForm
	{
		public string Name { get; set; }

		[RenderMode(RenderMode.None)]
		public User CurrentUser { get; set; }
	}
}