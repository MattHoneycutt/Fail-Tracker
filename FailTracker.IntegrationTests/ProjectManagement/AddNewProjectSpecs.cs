using FailTracker.Web.Controllers;
using FailTracker.Web.Models.ProjectAdministration;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using MvcContrib.TestHelper;
using Should;
using SpecsFor.Mvc.SeleniumExtensions;

namespace FailTracker.IntegrationTests.ProjectManagement
{
	public class AddNewProjectSpecs
	{
		public class when_adding_a_new_project : SpecsFor<MvcWebApp>
		{
			protected override void When()
			{
				SUT.NavigateTo<ProjectAdministrationController>(c => c.Index());
				SUT.FindLinkTo<ProjectAdministrationController>(c => c.AddProject()).ClickButton();

				SUT.FindFormFor<AddProjectForm>()
					.Field(f => f.Name).SetValueTo("My Test Project")
					.Submit();
			}

			[Test]
			public void then_it_returns_to_the_project_administration_page()
			{
				SUT.Route.ShouldMapTo<ProjectAdministrationController>(c => c.Index());
			}

			[Test]
			public void then_it_should_have_the_new_project_listed()
			{
				SUT.FindDisplayFor<ProjectListViewModel>()
					.DisplayFor(m => m.Summaries[0].Name).Text.ShouldEqual("My Test Project");
			}

			[Test]
			public void then_the_new_project_has_one_member()
			{
				SUT.FindDisplayFor<ProjectListViewModel>()
					.DisplayFor(m => m.Summaries[0].Members).Text.ShouldEqual("1");
			}

			[Test]
			public void then_the_new_project_has_no_issues()
			{
				SUT.FindDisplayFor<ProjectListViewModel>()
					.DisplayFor(m => m.Summaries[0].ActiveStories).Text.ShouldEqual("0");
			}
		}
	}
}