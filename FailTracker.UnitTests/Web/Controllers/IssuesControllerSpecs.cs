using System;
using System.Web.Mvc;
using Moq;
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
		public class when_requesting_the_list_of_issues : SpecsFor<IssuesController>
		{
			private ActionResult _result;

			protected override void Given()
			{
				GetMockFor<IRepository<Issue>>()
					.Setup(s => s.Query())
					.Returns((new[] {Issue.CreateNewIssue("Test1", User.CreateNewUser("test@user.com", "Blah"), "blah")}).AsQueryable());
			}

			protected override void When()
			{
				_result = SUT.Dashboard();
			}

			[Test]
			public void then_it_returns_a_view()
			{
				_result.AssertViewRendered().WithViewData<IssueViewModel[]>().ShouldNotBeEmpty();
			}
		}

		public class when_adding_a_new_issue : given.users_exists
		{
			private readonly Guid TestIssueID = Guid.NewGuid();
			private ActionResult _result;

			protected override void Given()
			{
				base.Given();

				var newStory = Issue.CreateNewIssue("Test Title", CreatorUser, "Content");
				newStory.ChangeSizeTo(PointSize.Eight);
				newStory.ReassignTo(TestUser);

				GetMockFor<IRepository<Issue>>()
					.Setup(s => s.Save(newStory))
					.Callback<Object>(i => ((Issue) i).ID = TestIssueID)
					.Verifiable();
			}

			protected override void When()
			{
				var form = new AddIssueForm
				                   	{
				                   		AssignedTo = TestUser.ID, 
				                   		Title = "Test Title", 
				                   		Body = "Content",
				                   		Size = PointSize.Eight
				                   	};
				_result = SUT.AddIssue(form);
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
		}

		public class when_adding_a_new_issue_that_is_not_assigned_to_anyone : given.users_exists
		{
			protected override void When()
			{
				SUT.AddIssue(new AddIssueForm {Title = "Test", Body = "Test"});
			}

			[Test]
			public void then_it_adds_the_issue_with_no_user_assigned()
			{
				GetMockFor<IRepository<Issue>>()
					.Verify(r => r.Save(It.Is<Issue>(i => i.AssignedTo == null)));
			}
		}

		public class when_adding_a_bug : given.users_exists
		{
			protected override void When()
			{
				SUT.AddIssue(new AddIssueForm {AssignedTo = TestUser.ID, Type = IssueType.Bug});
			}

			[Test]
			public void then_it_creates_a_bug()
			{
				GetMockFor<IRepository<Issue>>()
					.Verify(r => r.Save(It.Is<Issue>(i => i.Type == IssueType.Bug)));
			}
		}

		public class when_viewing_an_issue : SpecsFor<IssuesController>
		{
			private ActionResult _result;
			private Issue[] TestIssues;

			protected override void Given()
			{
				TestIssues = new[]
				             	{
				             		Issue.CreateNewIssue("Test 1", User.CreateNewUser("test@user1.com", "blah"), "Test 1 Body"),
				             		Issue.CreateNewIssue("Test 2", User.CreateNewUser("test@user2.com", "blah"), "Test 2 Body")
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

				data.AssignedTo.ShouldEqual(TestIssues[1].AssignedTo.EmailAddress);
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

		public class when_requesting_the_edit_page : given.users_and_issues_exist
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
				//TODO: Add AutoMapper to cut down on tedious testing. 
				var model = _result.AssertViewRendered().WithViewData<EditIssueForm>();
				model.AssignedTo.ShouldEqual(TestIssue.AssignedTo.ID);
			}
		}

		public class when_editing_an_issue : given.users_and_issues_exist
		{
			private ActionResult _result;

			protected override void When()
			{
				_result =SUT.Edit(new EditIssueForm
				         	{
				         		ID = TestIssue.ID,
				         		AssignedTo = CreatorUser.ID,
				         		Size = PointSize.OneHundred,
				         		Type = IssueType.Bug,
				         		Title = "Edited!",
				         		Comments = "Edited Comments!"
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
			public void then_it_adds_a_comment()
			{
				TestIssue.Changes.Last().Comment.ShouldEqual("Edited Comments!");
			}
		}

		#region Context

		public static class given
		{
			public abstract class users_exists : SpecsFor<IssuesController>
			{
				protected User TestUser;
				protected User CreatorUser;

				protected override void Given()
				{
					TestUser = User.CreateNewUser("test@user.com", "blah");
					TestUser.ID = Guid.NewGuid();

					CreatorUser = User.CreateNewUser("some@user.com", "blah");
					CreatorUser.ID = Guid.NewGuid();

					GetMockFor<IRepository<User>>()
						.Setup(s => s.Query())
						.Returns((new[] { CreatorUser, TestUser }).AsQueryable());

					var context = GetMockFor<ControllerContext>();
					context.SetupGet(c => c.HttpContext.User.Identity.Name).Returns("some@user.com");
					SUT.ControllerContext = context.Object;
				}
			}

			public abstract class users_and_issues_exist : users_exists
			{
				protected Issue TestIssue;

				protected override void Given()
				{
					base.Given();

					TestIssue = Issue.CreateNewIssue("Test Issue", CreatorUser, "This is a test");
					TestIssue.ReassignTo(TestUser);
					TestIssue.ID = Guid.NewGuid();

					GetMockFor<IRepository<Issue>>()
						.Setup(r => r.Query())
						.Returns((new[] { TestIssue }).AsQueryable());
				}
			}
		}

		#endregion
	}
}