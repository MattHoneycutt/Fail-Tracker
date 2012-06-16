using FailTracker.Web.Controllers;
using FailTracker.Web.Models.Authentication;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using MvcContrib.TestHelper;

namespace FailTracker.IntegrationTests.General
{
	public class AuthenticationSpecs
	{
		public abstract class given_the_user_is_unauthenticated : SpecsFor<MvcWebApp>
		{
			protected override void Given()
			{
				SUT.NavigateTo<AuthenticationController>(c => c.LogOff());
			}

			public class when_trying_to_access_the_dashboard_controller : given_the_user_is_unauthenticated
			{
				protected override void When()
				{
					SUT.NavigateTo<DashboardController>(c => c.Index());
				}

				[Test]
				public void then_it_redirects_to_the_login_page()
				{
					SUT.Route.ShouldMapTo<AuthenticationController>(c => c.LogOn());
				}
			}

			public class when_logging_in_with_no_credentials : given_the_user_is_unauthenticated
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

			public class when_logging_in_with_invalid_credentials : given_the_user_is_unauthenticated
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

			public class when_logging_in_with_valid_credentials : given_the_user_is_unauthenticated
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
				public void then_it_should_redirect_to_the_dashboard()
				{
					SUT.Route.ShouldMapTo<DashboardController>(c => c.Index());
				}
			}
		}

		public class when_logging_out : SpecsFor<MvcWebApp>
		{
			protected override void When()
			{
				SUT.NavigateTo<AuthenticationController>(c => c.LogOff());
			}

			[Test]
			public void then_it_takes_the_user_to_the_login_page()
			{
				SUT.Route.ShouldMapTo<AuthenticationController>(c => c.LogOn());
			}
		}
	}
}