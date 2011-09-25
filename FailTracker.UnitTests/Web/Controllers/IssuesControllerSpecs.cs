using System;
using System.Web.Mvc;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using NUnit.Framework;
using SpecsFor;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Controllers;
using MvcContrib.TestHelper;
using FailTracker.Web.Models.Issues;
using Should;
using System.Linq;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class IssuesControllerSpecs
	{
		public class when_viewing_an_issue : SpecsFor<IssuesController>
		{
			private ActionResult _result;
			private Issue[] TestIssues;
			private Project TestProject;

			protected override void Given()
			{
				TestProject = Project.Create("Test", User.CreateNewUser("creator@user.com", "blah"));

				TestIssues = new[]
				             	{
				             		Issue.CreateNewIssue(TestProject, "Test 1", User.CreateNewUser("test@user1.com", "blah"), "Test 1 Description"),
				             		Issue.CreateNewIssue(TestProject, "Test 2", User.CreateNewUser("test@user2.com", "blah"), "Test 2 Description")
										.ReassignTo(User.CreateNewUser("worker@bee.com", "blah"))
										.ChangeSizeTo(PointSize.Thirteen)
										.ChangeTypeTo(IssueType.Bug),
				             	};

				TestIssues[0].ID = Guid.NewGuid();
				TestIssues[1].ID = Guid.NewGuid();
				TestIssues[1].BeginEdit(User.CreateNewUser("test@user3.com", "pass"), "Edit 1!");
				TestIssues[1].BeginEdit(User.CreateNewUser("test@user3.com", "pass"), "Edit 2!");

				GetMockFor<IRepository<Issue>>()
					.Setup(r => r.Query())
					.Returns(TestIssues.AsQueryable());
			}

			protected override void When()
			{
				_result = SUT.View(TestIssues[1].ID);
			}

			[Test]
			public void then_it_renders_a_view_model_with_the_issue_data()
			{
				var data = _result.AssertViewRendered().WithViewData<IssueDetailsViewModel>();

				data.AssignedToEmailAddress.ShouldEqual(TestIssues[1].AssignedTo.EmailAddress);
				data.Size.ShouldEqual(TestIssues[1].Size);
				data.Type.ShouldEqual(TestIssues[1].Type);
			}

			[Test]
			public void then_it_returns_the_changes()
			{
				var data = _result.AssertViewRendered().WithViewData<IssueDetailsViewModel>();
				data.Changes.Length.ShouldEqual(2);
			}
		}

		public class when_requesting_the_edit_page : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Edit(TestIssue.ID);
			}

			[Test]
			public void then_it_renders_a_view()
			{
				_result.AssertViewRendered();
			}

			[Test]
			public void then_it_returns_the_expected_model()
			{
				var model = _result.AssertViewRendered().WithViewData<EditIssueForm>();
				model.AssignedToID.ShouldEqual(TestIssue.AssignedTo.ID);
			}
		}

		public class when_editing_an_issue : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result =SUT.Edit(new EditIssueForm
				         	{
				         		ID = TestIssue.ID,
				         		AssignedToID = CreatorUser.ID,
				         		Size = PointSize.OneHundred,
				         		Type = IssueType.Bug,
				         		Title = "Edited!",
								Description = "New Description",
				         		Comments = "Edited Comments!",
								CurrentUser = TestUser
				         	});
			}

			[Test]
			public void then_it_redirects_to_the_view_page()
			{
				_result.AssertActionRedirect().ToAction<IssuesController>(c => c.View(TestIssue.ID));
			}

			[Test]
			public void then_it_changes_the_owner()
			{
				TestIssue.AssignedTo.ShouldEqual(CreatorUser);
			}

			[Test]
			public void then_it_changes_the_point_size()
			{
				TestIssue.Size.ShouldEqual(PointSize.OneHundred);
			}

			[Test]
			public void then_it_changes_the_type()
			{
				TestIssue.Type.ShouldEqual(IssueType.Bug);
			}

			[Test]
			public void then_it_changes_the_title()
			{
				TestIssue.Title.ShouldEqual("Edited!");
			}

			[Test]
			public void then_it_changes_the_description()
			{
				TestIssue.Description.ShouldEqual("New Description");
			}

			[Test]
			public void then_it_adds_a_comment()
			{
				TestIssue.Changes.Last().Comments.ShouldEqual("Edited Comments!");
			}

			[Test]
			public void then_it_captures_the_editor()
			{
				TestIssue.Changes.Last().EditedBy.ShouldEqual(TestUser);
			}
		}

		public class when_getting_issue_details : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Details(TestIssue.ID);
			}

			[Test]
			public void then_it_returns_a_populated_view()
			{
				_result.AssertViewRendered().WithViewData<IssueDetailsViewModel>().ShouldNotBeNull();
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<IssuesController>
			{
				
			}
		}
	}
}