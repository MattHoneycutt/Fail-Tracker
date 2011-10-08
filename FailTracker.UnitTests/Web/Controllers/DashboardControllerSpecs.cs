using System.Linq;
using System.Web.Mvc;
using FailTracker.Core.Domain;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.Dashboard;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Should;
using NHibernate.Linq;

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
			public void then_each_project_has_issues()
			{
				var result = (ViewResult)_result;
				var model = (ProjectDashboardViewModel[]) result.Model;

				model.Any(m => m.ActiveIssues.Count() > 0).ShouldBeTrue();
			}
		}

		public class when_a_project_has_completed_issues : given.project_has_completed_issues
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Index();
			}

			[Test]
			public void then_no_issues_are_returned()
			{
				_result.AssertViewRendered()
					.WithViewData<ProjectDashboardViewModel[]>()[1]
					.ActiveIssues.ShouldBeEmpty();
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<DashboardController>
			{
			}

			public abstract class project_has_completed_issues : the_default_state
			{
				protected override void Given()
				{
					base.Given();

					TestProject.CurrentIssues.Where(i => i.Status != Status.Complete).ForEach(i => i.Complete(TestUser, "Blah"));
				}
			}
		}
	}
}