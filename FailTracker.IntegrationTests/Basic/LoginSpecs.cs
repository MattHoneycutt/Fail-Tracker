using FailTracker.Web.Controllers;
using FailTracker.Web.Models.Authentication;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using MvcContrib.TestHelper;

namespace FailTracker.IntegrationTests.Basic
{
	public class LoginSpecs
	{
		public class when_logging_in_with_no_credentials : SpecsFor<MvcWebApp>
		{
			protected override void When()
			{
				SUT.NavigateTo<AuthenticationController>(c => c.LogOn());
				SUT.FindFormFor<LogOnForm>().Submit();
			}

			[Test]
			public void then_it_displays_a_validation_error_on_the_email_address()
			{
				SUT.FindFormFor<LogOnForm>()
					.Field(f => f.EmailAddress).ShouldBeInvalid();
			}

			[Test]
			public void then_it_displays_a_validation_error_on_the_password()
			{
				SUT.FindFormFor<LogOnForm>()
					.Field(f => f.Password).ShouldBeInvalid();
			}
		}

		public class when_logging_in_with_invalid_credentials : SpecsFor<MvcWebApp>
		{
			protected override void When()
			{
				SUT.NavigateTo<AuthenticationController>(c => c.LogOn());
				SUT.FindFormFor<LogOnForm>()
					.Field(f => f.EmailAddress).SetValueTo("Blah@test.com")
					.Field(f => f.Password).SetValueTo("Blah")
					.Submit();
			}

			[Test]
			public void then_it_should_not_log_you_in()
			{
				SUT.Route.ShouldMapTo<AuthenticationController>(c => c.LogOn());
			}
		}

		public class when_logging_in_with_valid_credentials : SpecsFor<MvcWebApp>
		{
			protected override void When()
			{
				SUT.NavigateTo<AuthenticationController>(c => c.LogOn());
				SUT.FindFormFor<LogOnForm>()
					.Field(f => f.EmailAddress).SetValueTo("test@user.com")
					.Field(f => f.Password).SetValueTo("TestPassword01")
					.Submit();
			}

			[Test]
			public void then_it_should_redirect_to_the_home_page()
			{
				SUT.Route.ShouldMapTo<DashboardController>(c => c.Index());
			}
		}
	}
}