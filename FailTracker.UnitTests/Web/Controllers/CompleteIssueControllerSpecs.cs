using System.Web.Mvc;
using FailTracker.Core.Domain;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.CompleteIssue;
using NUnit.Framework;
using SpecsFor;
using MvcContrib.TestHelper;
using Should;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class CompleteIssueControllerSpecs
	{
		public class when_preparing_to_complete_an_issue : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Complete(TestIssue.ID);
			}

			[Test]
			public void then_it_returns_a_view_for_the_issue()
			{
				_result.AssertViewRendered()
					.WithViewData<CompleteIssueForm>()
					.Title.ShouldEqual(TestIssue.Title);
			}
		}

		public class when_completing_an_issue : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Complete(new CompleteIssueForm {ID = TestIssue.ID, Comments = "Issue is fixed"});
			}

			[Test]
			public void then_it_marks_the_issue_as_complete()
			{
				TestIssue.Status.ShouldEqual(Status.Complete);
			}

			[Test]
			public void then_it_redirects_to_the_view_page()
			{
				_result.AssertActionRedirect().ToAction<IssuesController>(c => c.View(TestIssue.ID));
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<CompleteIssueController>
			{
		
			}
		}
	}
}