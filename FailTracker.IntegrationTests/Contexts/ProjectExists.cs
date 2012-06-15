using FailTracker.Web.Controllers;
using FailTracker.Web.Models.ProjectAdministration;
using SpecsFor;
using SpecsFor.Mvc;

namespace FailTracker.IntegrationTests.Contexts
{
	public class ProjectExists : IContext<MvcWebApp>
	{
		private readonly string _projectName;

		public ProjectExists(string projectName)
		{
			_projectName = projectName;
		}

		public void Initialize(ITestState<MvcWebApp> state)
		{
			state.SUT.NavigateTo<ProjectAdministrationController>(c => c.AddProject());
			state.SUT.FindFormFor<AddProjectForm>()
				.Field(f => f.Name).SetValueTo(_projectName)
				.Submit();
		}
	}
}