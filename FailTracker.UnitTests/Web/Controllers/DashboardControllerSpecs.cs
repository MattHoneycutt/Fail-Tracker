using System.Linq;
using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.Dashboard;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class DashboardControllerSpecs
	{
		public class when_getting_the_dashboard : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Index();
			}

			[Test]
			public void then_it_returns_a_view()
			{
				_result.AssertViewRendered().WithViewData<ProjectDashboardViewModel[]>().ShouldNotBeEmpty();
			}

			[Test]
			public void then_each_view_has_issues()
			{
				var result = (ViewResult)_result;
				var model = (ProjectDashboardViewModel[]) result.Model;

				model.All(m => m.CurrentIssues.Count() > 0).ShouldBeTrue();
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsFor<DashboardController>
			{
				protected override void Given()
				{
					GetMockFor<IRepository<Project>>()
						.Setup(s => s.Query())
						.Returns((new[] { BuildTestProject(), BuildTestProject() }).AsQueryable());
				}

				private Project BuildTestProject()
				{
					var project = Project.Create("Test Project");

					Issue.CreateNewIssue(project, "Test Title", User.CreateNewUser("test@user.com", "blah"), "Body!");

					return project;
				}
			}
		}
	}
}