using System;
using FailTracker.Core.Domain;
using NUnit.Framework;
using SpecsFor;
using Should;

namespace FailTracker.UnitTests.Core.Domain
{
	public class IssueSpecs
	{
		public class when_reassigning_an_issue : SpecsFor<Issue>
		{
			private User TestUser;

			private Issue _result;

			protected override void InitializeClassUnderTest()
			{
				SUT = Issue.CreateNewIssue("My issue", User.CreateNewUser("test", "blah"), "Body");
			}

			protected override void Given()
			{
				TestUser = User.CreateNewUser("test@user.com", "12345");
			}

			protected override void When()
			{
				_result = SUT.ReassignTo(TestUser);
			}

			[Test]
			public void then_it_reassigns_the_issue()
			{
				SUT.AssignedTo.ShouldEqual(TestUser);
			}

			[Test]
			public void then_it_returns_the_SUT()
			{
				_result.ShouldBeSameAs(SUT);
			}
		}

		public class when_creating_a_new_issue : SpecsFor<Issue>
		{
			protected override void InitializeClassUnderTest()
			{
				SUT = null;
			}

			protected override void When()
			{
				SUT = Issue.CreateNewIssue("Testing", User.CreateNewUser("creator@user.com", "test"), "Test body");
			}

			[Test]
			public void then_the_issue_is_unassigned_by_default()
			{
				SUT.IsUnassigned.ShouldBeTrue();
			}

			[Test]
			public void then_the_assigned_to_property_is_blank()
			{
				SUT.AssignedTo.ShouldBeNull();
			}

			[Test]
			public void then_the_creator_is_captured()
			{
				SUT.CreatedBy.EmailAddress.ShouldEqual("creator@user.com");
			}

			[Test]
			public void then_it_sets_the_date_created()
			{
				SUT.CreatedAt.ShouldBeInRange(DateTime.Now.AddSeconds(-5), DateTime.Now);
			}
		}

		public class when_changing_the_title : SpecsFor<Issue>
		{
			protected override void InitializeClassUnderTest()
			{
				SUT = Issue.CreateNewIssue("Test", null, "Body");
			}

			protected override void When()
			{
				SUT.ChangeTitleTo("Edited!");
			}

			[Test]
			public void then_it_updates_the_title()
			{
				SUT.Title.ShouldEqual("Edited!");
			}
		}
	}	
}