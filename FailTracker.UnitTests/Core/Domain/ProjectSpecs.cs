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

		public class when_attaching_an_issue_to_a_story : given.project_exists
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
		}
	}
}