using System;
using System.Web.Mvc;
using NUnit.Framework;
using SpecsFor;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;
using TryCatchFail.CodeStock2011.FailTracker.Web.Controllers;
using MvcContrib.TestHelper;
using TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues;
using Should;
using System.Linq;

namespace TryCatchFail.CodeStock2011.UnitTests.Web.Controllers
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
					.Returns((new[] {Issue.Create("Test1", User.CreateNewUser("test@user.com", "Blah"), "blah")}).AsQueryable());
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

		public class when_adding_a_new_issue : SpecsFor<IssuesController>
		{
			private readonly Guid TestIssueID = Guid.NewGuid();
			private User TestUser;

			private ActionResult _result;

			protected override void Given()
			{
				TestUser = User.CreateNewUser("test@user.com", "blah");
				TestUser.ID = Guid.NewGuid();

				GetMockFor<IRepository<Issue>>()
					.Setup(s => s.Save(Issue.Create("Test Title", TestUser, "Content")))
					.Callback<Object>(i => ((Issue) i).ID = TestIssueID)
					.Verifiable();

				GetMockFor<IRepository<User>>()
					.Setup(s => s.Query())
					.Returns((new[] {User.CreateNewUser("some@user.com", "blah"), TestUser}).AsQueryable());
			}

			protected override void When()
			{
				_result = SUT.AddIssue(new AddIssueForm {AssignedTo = TestUser.ID, Title = "Test Title", Body = "Content"});
			}

			[Test]
			public void then_it_will_send_an_add_issue_command()
			{
				GetMockFor<IRepository<Issue>>().Verify();
			}

			[Test]
			public void then_it_returns_a_redirect_to_the_view_issue_action_for_the_new_issue()
			{
				_result.AssertActionRedirect().ToAction<IssuesController>(c => c.View(TestIssueID));
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
				             		Issue.Create("Test 1", User.CreateNewUser("test@user1.com", "blah"), "Test 1 Body"),
				             		Issue.Create("Test 2", User.CreateNewUser("test@user2.com", "blah"), "Test 2 Body"),
				             	};

				TestIssues[0].ID = Guid.NewGuid();
				TestIssues[1].ID = Guid.NewGuid();

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
				_result.AssertViewRendered()
					.WithViewData<ViewIssueViewModel>()
					.AssignedTo.ShouldEqual("test@user2.com");
			}
		}
	}
}