using System.Web.Mvc;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.Backlog;
using NUnit.Framework;
using MvcContrib.TestHelper;
using Should;
using System.Linq;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class BacklogControllerSpecs
	{
		public class when_viewing_the_full_backlog : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Index(TestProject.ID);
			}

			[Test]
			public void then_it_returns_a_view()
			{
				_result.AssertViewRendered();
			}

			[Test]
			public void then_the_view_contains_a_list_of_all_active_issues()
			{
				_result.AssertViewRendered()
					.WithViewData<ProjectBacklogViewModel>()
					.Issues.Length.ShouldEqual(TestProject.CurrentIssues.Count());
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<BacklogController>
			{
				
			}
		}
	}
}