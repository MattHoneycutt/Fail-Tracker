using System.Linq;
using System.Web.Mvc;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.Dashboard;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Should;

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

				model.Any(m => m.CurrentIssues.Count() > 0).ShouldBeTrue();
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<DashboardController>
			{
			}
		}
	}
}