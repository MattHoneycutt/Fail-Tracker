using System;
using System.Web.Mvc;
using Moq;
using NHibernate;
using NUnit.Framework;
using SpecsFor;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;
using TryCatchFail.CodeStock2011.FailTracker.Core.Features.Dashboard;
using TryCatchFail.CodeStock2011.FailTracker.Web.Controllers;
using MvcContrib.TestHelper;
using TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues;
using Should;
using TryCatchFail.CodeStock2011.UnitTests.Util;

namespace TryCatchFail.CodeStock2011.UnitTests.Web.Controllers
{
	public class IssueControllerSpecs
	{
		public class when_requesting_the_list_of_issues : SpecsFor<IssuesController>
		{
			private ActionResult _result;

			protected override void Given()
			{
				this.GetQueryMockFor<GetIssueHeadersQuery>()
					.Setup(q => q.Run(It.IsAny<GetIssueHeadersQuery.Options>()))
					.Returns(new[] {new GetIssueHeadersQuery.Result()});
			}

			protected override void When()
			{
				//TODO: Rename to dashboard? 
				_result = SUT.Index();
			}

			[Test]
			public void then_it_returns_a_view()
			{
				_result.AssertViewRendered().WithViewData<IssueViewModel[]>().ShouldNotBeEmpty();
			}
		}
		
		public class when_adding_a_new_issue : SpecsFor<IssuesController>
		{
			private ActionResult _result;
			private Guid TestIssueID = Guid.NewGuid();

			protected override void Given()
			{
				GetMockFor<ISession>()
					.Setup(s => s.Save(It.IsAny<Issue>()))
					.Callback<Object>(i => ((Issue)i).ID = TestIssueID);
			}

			protected override void When()
			{
				_result = SUT.AddIssue(new AddIssueForm {AssignedTo = "Matt", Title = "Test Title", Body = "Content"});
			}

			[Test]
			public void then_it_will_send_an_add_issue_command()
			{
				GetMockFor<ISession>()
					.Verify(s => s.Save(new Issue {ID = TestIssueID, AssignedTo = "Matt", Title = "Test Title", Body = "Content"}));
			}

			[Test]
			public void then_it_returns_a_redirect_to_the_view_issue_action_for_the_new_issue()
			{
				_result.AssertActionRedirect().ToAction<IssuesController>(c => c.View(TestIssueID));
			}
		}
	}
}