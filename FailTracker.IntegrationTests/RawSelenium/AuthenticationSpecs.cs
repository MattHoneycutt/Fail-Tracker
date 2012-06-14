using MvcContrib.TestHelper;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecsFor;
using SpecsFor.Mvc;
using Should;

namespace FailTracker.IntegrationTests.RawSelenium
{
	public class AuthenticationSpecs
	{
		public abstract class given_the_user_is_unauthenticated : SpecsFor<MvcWebApp>
		{
			protected override void Given()
			{
				SUT.Browser.Navigate().GoToUrl(MvcWebApp.BaseUrl + "/Authentication/LogOff");
			}

			public class when_trying_to_access_the_dashboard_controller : given_the_user_is_unauthenticated
			{
				protected override void When()
				{
					SUT.Browser.Navigate().GoToUrl(MvcWebApp.BaseUrl + "/Dashboard");
				}

				[Test]
				public void then_it_redirects_to_the_login_page()
				{
					SUT.Browser.Url.ShouldEqual(MvcWebApp.BaseUrl + "/LogOn?ReturnUrl=%2fDashboard");
				}
			}

			public class when_logging_in_with_no_credentials : given_the_user_is_unauthenticated
			{
				protected override void When()
				{
					SUT.Browser.Navigate().GoToUrl(MvcWebApp.BaseUrl + "/LogOn");
					SUT.Browser.FindElement(By.TagName("form")).Submit();
				}

				[Test]
				public void then_it_displays_a_validation_error_on_the_email_address()
				{
					SUT.Browser.FindElements(By.CssSelector("span.field-validation-error[data-valmsg-for=\"EmailAddress\"]")).ShouldNotBeEmpty();
				}

				[Test]
				public void then_it_displays_a_validation_error_on_the_password()
				{
					SUT.Browser.FindElements(By.CssSelector("span.field-validation-error[data-valmsg-for=\"Password\"]")).ShouldNotBeEmpty();
				}
			}

			public class when_logging_in_with_invalid_credentials : given_the_user_is_unauthenticated
			{
				protected override void When()
				{
					SUT.Browser.Navigate().GoToUrl(MvcWebApp.BaseUrl + "/LogOn");
					SUT.Browser.FindElement(By.Id("EmailAddress")).SendKeys("Blah@test.com");
					SUT.Browser.FindElement(By.Id("Password")).SendKeys("Blah");
					SUT.Browser.FindElement(By.TagName("form")).Submit();
				}

				[Test]
				public void then_it_should_not_log_you_in()
				{
					SUT.Browser.Url.ShouldEqual(MvcWebApp.BaseUrl + "/LogOn");
				}
			}

			public class when_logging_in_with_valid_credentials : given_the_user_is_unauthenticated
			{
				protected override void When()
				{
					SUT.Browser.Navigate().GoToUrl(MvcWebApp.BaseUrl + "/LogOn");
					SUT.Browser.FindElement(By.Id("EmailAddress")).SendKeys("test@user.com");
					SUT.Browser.FindElement(By.Id("Password")).SendKeys("TestPassword01");
					SUT.Browser.FindElement(By.TagName("form")).Submit();
				}

				[Test]
				public void then_it_should_redirect_to_the_home_page()
				{
					SUT.Browser.Url.ShouldEqual(MvcWebApp.BaseUrl + "/");
				}
			}
		}
	}
}