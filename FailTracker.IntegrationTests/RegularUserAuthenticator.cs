using FailTracker.Web.Controllers;
using FailTracker.Web.Models.Authentication;
using SpecsFor.Mvc;
using SpecsFor.Mvc.Authentication;

namespace FailTracker.IntegrationTests
{
	public class RegularUserAuthenticator : IHandleAuthentication
	{
		public void Authenticate(MvcWebApp app)
		{
			app.NavigateTo<AuthenticationController>(c => c.LogOn());
			app.Browser.Manage().Cookies.DeleteAllCookies();
			
			app.FindFormFor<LogOnForm>().Field(f => f.EmailAddress)
				.SetValueTo("test@user.com").Field(f => f.Password)
				.SetValueTo("TestPassword01")
				.Submit();
		}
	}
}