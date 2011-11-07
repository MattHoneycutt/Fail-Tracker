using System;
using System.Linq;
using FailTracker.Core.Domain;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace FailTracker.UnitTests.Core.Domain
{
	public class ProjectSpecs
	{
		public class when_creating_a_new_project : given.project_exists
		{
			protected override void When()
			{
				//Project already created!
			}

			[Test]
			public void then_it_assigns_an_owner()
			{
				SUT.Creator.ShouldEqual(Creator);
			}

			[Test]
			public void then_the_creator_is_added_to_the_members_list()
			{
				SUT.Members.ShouldContain(Creator);
			}
		}

		public class when_adding_a_user_to_a_project : given.project_exists
		{
			private User TestUser;

			protected override void When()
			{
				TestUser = User.CreateNewUser("test@blach.com", "test");
				SUT.AddMember(TestUser);
			}

			[Test]
			public void then_it_adds_the_user_to_the_member_list()
			{
				SUT.Members.ShouldContain(TestUser);
			}
		}

		public class when_attaching_an_issue_to_a_project : given.project_exists
		{
			private Issue TestIssue;

			protected override void When()
			{
				TestIssue = Issue.CreateNewIssue(SUT, "Test", Creator, "blah");
				SUT.AttachIssue(TestIssue);
			}

			[Test]
			public void then_it_adds_the_issue_to_the_list_of_current_issues()
			{
				SUT.CurrentIssues.ShouldContain(TestIssue);
			}
		}

		public class when_moving_an_issue_to_the_top_of_the_backlog : given.there_are_multiple_issues_in_the_project
		{
			protected override void When()
			{
				SUT.MoveIssue(LastIssue, MoveType.Before, FirstIssue);
			}

			[Test]
			public void then_the_order_of_issues_is_updated()
			{
				SUT.CurrentIssues.ToArray().ShouldEqual(new[] { LastIssue, FirstIssue});
			}
		}

		public class when_moving_an_issue_that_does_not_belong_to_the_backlog : given.there_are_multiple_issues_in_the_project
		{
			private InvalidOperationException _exception;

			protected override void When()
			{
				var newIssue = Issue.CreateNewIssue(Project.Create("Other", Creator), "Not In Project", Creator, "Test");
				_exception = Assert.Throws<InvalidOperationException>(() => SUT.MoveIssue(newIssue, MoveType.Before, FirstIssue));
			}

			[Test]
			public void then_it_throws_an_exception_that_explains_how_to_fix_the_problem()
			{
				_exception.Message.ShouldEqual("The issue you are attempting to move does not belong to the project.  Attach it to the project first, then try moving it.");
			}
		}

		public class when_moving_an_issue_before_a_target_that_is_not_in_the_backlog : given.there_are_multiple_issues_in_the_project
		{
			private InvalidOperationException _exception;

			protected override void When()
			{
				var newIssue = Issue.CreateNewIssue(Project.Create("Other", Creator), "Not In Project", Creator, "Test");
				_exception = Assert.Throws<InvalidOperationException>(() => SUT.MoveIssue(LastIssue, MoveType.Before, newIssue));
			}

			[Test]
			public void then_it_throws_an_exception_that_explains_how_to_fix_the_problem()
			{
				_exception.Message.ShouldEqual("The target issue does not belong to the project.  Attach it to the project first, then try moving an issue before it.");
			}
		}

		public class when_moving_the_first_issue_to_the_bottom_of_the_backlog : given.there_are_multiple_issues_in_the_project
		{
			protected override void When()
			{
				SUT.MoveIssue(FirstIssue, MoveType.After, LastIssue);
			}

			[Test]
			public void then_it_reorders_the_backlog()
			{
				SUT.CurrentIssues.ToArray().ShouldEqual(new[] {LastIssue, FirstIssue});
			}
		}

		public static class given
		{
			public abstract class project_exists : SpecsFor<Project>
			{
				protected User Creator;

				protected override void InitializeClassUnderTest()
				{
					Creator = User.CreateNewUser("test@user.com", "blah");
					SUT = Project.Create("Test Project", Creator);
				}
			}

			public abstract class there_are_multiple_issues_in_the_project : project_exists
			{
				protected Issue FirstIssue;
				protected Issue LastIssue;

				protected override void Given()
				{
					base.Given();

					FirstIssue = Issue.CreateNewIssue(SUT, "An Issue", Creator, "Test");
					LastIssue = Issue.CreateNewIssue(SUT, "Another Issue", Creator, "Test");
				}
			}

		}
	}
}