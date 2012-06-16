using FailTracker.IntegrationTests.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.SupportRequest;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using MvcContrib.TestHelper;
using Should;
using SpecsFor.Mvc.Smtp;
using SpecsFor.ShouldExtensions;

namespace FailTracker.IntegrationTests.SupportRequests
{
	public class SubmitNewRequest
	{
		public class when_a_new_support_request_is_submitted : SpecsFor<MvcWebApp>
		{
			protected override void Given()
			{
				Given<UserIsUnauthenticated>();
				base.Given();
			}

			protected override void When()
			{
				SUT.NavigateTo<SupportRequestController>(c => c.Submit());
				SUT.FindFormFor<SubmitSupportRequestForm>()
					.Field(f => f.Subject).SetValueTo("Fail Tracker ROCKS")
					.Field(f => f.From).SetValueTo("number1fan@failtrackerfans.com")
					.Field(f => f.Body).SetValueTo("You guys frickin' rule!")
					.Submit();
			}

			[Test]
			public void then_it_acknowledges_the_submission()
			{
				SUT.Route.ShouldMapTo<SupportRequestController>(c => c.ThankYou());
				SUT.FindDisplayFor<SupportAcknowledgementViewModel>()
					.DisplayFor(m => m.From).Text.ShouldEqual("number1fan@failtrackerfans.com");
			}

			[Test]
			public void then_it_sends_the_devs_an_email()
			{
				SUT.Mailbox().MailMessages.ShouldContain(m => m.To[0].Address == "mbhoneycutt@gmail.com");
			}
		}
	}
}