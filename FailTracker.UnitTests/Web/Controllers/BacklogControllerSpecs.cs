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
				_result = SUT.Active(TestProject.ID);
			}

			[Test]
			public void then_it_returns_a_view()
			{
				_result.AssertViewRendered();
			}

			[Test]
			public void then_the_view_contains_only_active_issues()
			{
				_result.AssertViewRendered()
					.WithViewData<BacklogStoriesViewModel>()
					.Issues.Select(i => i.ID).ToArray().ShouldEqual(new[] { ActiveIssue.ID} );
			}
		}

		public class when_viewing_completed_stories : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Completed(TestProject.ID);
			}

			[Test]
			public void then_it_returns_a_view()
			{
				_result.AssertViewRendered();
			}

			[Test]
			public void then_it_returns_only_completed_issues()
			{
				_result.AssertViewRendered()
					.WithViewData<CompletedStoriesViewModel>()
					.Issues.Select(i => i.ID).ToArray().ShouldEqual(new[] { CompletedIssue.ID });
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