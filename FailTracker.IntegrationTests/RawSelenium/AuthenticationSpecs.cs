using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Should;

namespace FailTracker.IntegrationTests.RawSelenium
{
	/*
	 * NOTE: To run these tests, you must do the following:
	 * 1) Debug FailTracker.Web, and note it's URL.
	 * 1a) Change your web.config's connection string to point to the FailTrackerTest database.  Ask Steve for the script.
	 * 2) Copy and paste that URL into TargetAppUrl constant below.
	 * 3) Make sure the test@user.com's password is really "TestPassword01"
	 * 4) Add one project named "Test"
	 * 5) Add a user named "other@user.com"
	 * 6) Cross your fingers...
	 */
	[TestFixture]
	[Explicit]
	public class AuthenticationSpecs
	{
        private const string TargetAppUrl = "URL GOES HERE!";

		[Test]
		public void unauthorized_user_cannot_access_dashboard()
		{
			var capabilities = new DesiredCapabilities();
			capabilities.SetCapability(InternetExplorerDriver.IntroduceInstabilityByIgnoringProtectedModeSettings, true);

			var driver = new InternetExplorerDriver(capabilities);
			driver.Navigate().GoToUrl(TargetAppUrl + "/Authentication/LogOff");

			try
			{
				driver.Navigate().GoToUrl(TargetAppUrl + "/Dashboard");

				driver.Url.ShouldEqual(TargetAppUrl + "/LogOn?ReturnUrl=%2fDashboard");
			}
			finally
			{
				driver.Close();
			}
		}

		[Test]
		public void logging_in_with_no_credentials_displays_validation_error()
		{
			var capabilities = new DesiredCapabilities();
			capabilities.SetCapability(InternetExplorerDriver.IntroduceInstabilityByIgnoringProtectedModeSettings, true);

			var driver = new InternetExplorerDriver(capabilities);
			driver.Navigate().GoToUrl(TargetAppUrl + "/Authentication/LogOff");

			try
			{
				driver.Navigate().GoToUrl(TargetAppUrl + "/LogOn");

				driver.FindElement(By.TagName("form")).Submit();

				driver.Url.ShouldEqual(TargetAppUrl + "/LogOn");

				driver.FindElements(By.CssSelector("span.field-validation-error[data-valmsg-for=\"EmailAddress\"]")).ShouldNotBeEmpty();
				driver.FindElements(By.CssSelector("span.field-validation-error[data-valmsg-for=\"Password\"]")).ShouldNotBeEmpty();
			}
			finally
			{
				driver.Close();
			}
		}

		[Test]
		public void logging_in_with_invalid_credentials()
		{
			var capabilities = new DesiredCapabilities();
			capabilities.SetCapability(InternetExplorerDriver.IntroduceInstabilityByIgnoringProtectedModeSettings, true);

			var driver = new InternetExplorerDriver(capabilities);
			driver.Navigate().GoToUrl(TargetAppUrl + "/Authentication/LogOff");

			try
			{
				driver.Navigate().GoToUrl(TargetAppUrl + "/LogOn");

				driver.FindElement(By.Name("EmailAddrss")).SendKeys("bad@user.com");
				driver.FindElement(By.Name("Password")).SendKeys("BadPass");
				driver.FindElement(By.TagName("form")).Submit();

				driver.Url.ShouldEqual(TargetAppUrl + "/LogOn");

				driver.FindElement(By.ClassName("validation-summary-errors")).Text.ShouldContain(
					"The user name or password provided is incorrect.");
			}
			finally
			{
				driver.Close();
			}
		}

		[Test]
		public void logging_in_with_valid_credentials_redirects_to_the_dashboard()
		{
			var capabilities = new DesiredCapabilities();
			capabilities.SetCapability(InternetExplorerDriver.IntroduceInstabilityByIgnoringProtectedModeSettings, true);

			var driver = new InternetExplorerDriver(capabilities);
			driver.Navigate().GoToUrl(TargetAppUrl + "/Authentication/LogOff");

			try
			{
				driver.Navigate().GoToUrl(TargetAppUrl + "/LogOn");

				driver.FindElement(By.Name("EmailAddress")).SendKeys("test@user.com");
				driver.FindElement(By.Name("Password")).SendKeys("TestPassword01");
				driver.FindElement(By.TagName("form")).Submit();

				driver.Url.ShouldEqual(TargetAppUrl + "/");
			}
			finally
			{
				driver.Close();
			}
		}
	}
}