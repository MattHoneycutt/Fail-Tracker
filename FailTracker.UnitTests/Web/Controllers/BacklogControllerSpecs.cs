using System;
using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
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
					.Issues.Select(i => i.ID).ToArray().ShouldEqual(new[] { ActiveIssue.ID, LastIssue.ID } );
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

		public class when_reordering_the_backlog : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Reorder(new ReorderForm {MovedID = LastIssue.ID, RelativeToID = ActiveIssue.ID, ProjectID = TestProject.ID});
			}

			[Test]
			public void then_it_returns_success()
			{
				_result.ShouldBeType<JsonResult>();
			}

			[Test]
			public void then_it_puts_the_last_issue_at_the_top_of_the_backlog()
			{
				TestProject.CurrentIssues.First().ShouldBeSameAs(LastIssue);
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<BacklogController>
			{
				protected Issue LastIssue;

				protected override void Given()
				{
					base.Given();

					LastIssue = Issue.CreateNewIssue(TestProject, "Last Issue", CreatorUser, "Test");
					LastIssue.ID = Guid.NewGuid();

					GetMockFor<IRepository<Issue>>()
						.Setup(r => r.Query())
						.Returns((new[] {ActiveIssue, CompletedIssue, LastIssue}).AsQueryable());
				}
			}
		}
	}
}