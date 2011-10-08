using System.Web.Mvc;
using FailTracker.Core.Domain;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.ReactivateIssue;
using NUnit.Framework;
using SpecsFor;
using MvcContrib.TestHelper;
using Should;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class ReactivateIssueControllerSpecs
	{
		public class when_preparing_to_reactivate_an_issue : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Reactivate(ActiveIssue.ID);
			}

			[Test]
			public void then_it_displays_the_reactivation_form()
			{
				_result.AssertViewRendered()
					.WithViewData<ReactivateIssueForm>()
					.Title.ShouldEqual(ActiveIssue.Title);
			}
		}

		public class when_reactivating_an_issue : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Reactivate(new ReactivateIssueForm {ID = ActiveIssue.ID, Comments = "Issue wasn't fixed."});
			}

			[Test]
			public void then_it_changes_the_issues_status_to_active()
			{
				ActiveIssue.Status.ShouldEqual(Status.NotStarted);
			}

			[Test]
			public void then_it_redirects_to_the_issue()
			{
				_result.AssertActionRedirect().ToAction<IssuesController>(c => c.View(ActiveIssue.ID));
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<ReactivateIssueController>
			{
				protected override void Given()
				{
					base.Given();

					ActiveIssue.Complete(TestUser, "Completed by default_state");
				}
			}
		}
	}
}