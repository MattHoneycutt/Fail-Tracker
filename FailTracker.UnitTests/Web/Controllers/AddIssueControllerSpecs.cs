using System;
using System.Linq;
using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.AddIssue;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Should;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class AddIssueControllerSpecs
	{
		public class when_adding_a_new_issue : given.the_default_state
		{
			private readonly Guid TestIssueID = Guid.NewGuid();
			private ActionResult _result;

			protected override void Given()
			{
				base.Given();

				var expectedIssue = Issue.CreateNewIssue(Project.Create("Test", CreatorUser), "Test Title", CreatorUser, "Content");
				expectedIssue.ChangeSizeTo(PointSize.Eight);
				expectedIssue.ReassignTo(TestUser);

				GetMockFor<IRepository<Issue>>()
					.Setup(s => s.Save(expectedIssue))
					.Callback<Object>(i => ((Issue)i).ID = TestIssueID)
					.Verifiable();
			}

			protected override void When()
			{
				var form = new AddIssueForm
				{
					AssignedTo = TestUser.ID,
					Title = "Test Title",
					Body = "Content",
					Size = PointSize.Eight,
					CurrentUser = CreatorUser,
					TargetProjectID = TestProject.ID
				};
				_result = SUT.Index(form);
			}

			[Test]
			public void then_it_will_save_the_issue()
			{
				GetMockFor<IRepository<Issue>>().Verify();
			}

			[Test]
			public void then_it_returns_a_redirect_to_the_view_issue_action_for_the_new_issue()
			{
				_result.AssertActionRedirect().ToAction<IssuesController>(c => c.View(TestIssueID));
			}

			[Test]
			public void then_it_adds_the_issue_to_the_project()
			{
				TestProject.CurrentIssues.Single(i => i.ID == TestIssueID).ShouldNotBeNull();
			}
		}

		public class when_adding_a_new_issue_that_is_not_assigned_to_anyone : given.the_default_state
		{
			protected override void When()
			{
				SUT.Index(new AddIssueForm { Title = "Test", Body = "Test", TargetProjectID = TestProject.ID });
			}

			[Test]
			public void then_it_adds_the_issue_with_no_user_assigned()
			{
				GetMockFor<IRepository<Issue>>()
					.Verify(r => r.Save(It.Is<Issue>(i => i.AssignedTo == null)));
			}
		}

		public class when_adding_a_bug : given.the_default_state
		{
			protected override void When()
			{
				SUT.Index(new AddIssueForm { AssignedTo = TestUser.ID, Type = IssueType.Bug, TargetProjectID = TestProject.ID });
			}

			[Test]
			public void then_it_creates_a_bug()
			{
				GetMockFor<IRepository<Issue>>()
					.Verify(r => r.Save(It.Is<Issue>(i => i.Type == IssueType.Bug)));
			}
		}
		
		public class when_requesting_the_form : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Index(TestProject.ID);
			}

			[Test]
			public void then_it_renders_a_form_with_the_target_project()
			{
				_result.AssertViewRendered().WithViewData<AddIssueForm>().TargetProjectID.ShouldEqual(TestProject.ID);
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<AddIssueController>
			{

			}
		}
	}
}