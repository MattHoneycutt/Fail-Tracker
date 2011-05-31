using System.Linq;
using System.Web.Mvc;
using FailTracker.Web.Controllers;
using NUnit.Framework;

namespace FailTracker.UnitTests.Web
{
	[TestFixture]
	public class AuthorizationConventionVerifier
	{
		[Test]
		public void All_controllers_except_account_controller_inherit_from_authorize_base_controller()
		{
			var controllers = from c in typeof (AuthorizeBaseController).Assembly.GetExportedTypes()
			                  where !c.IsAbstract &&
			                        typeof (Controller).IsAssignableFrom(c)
			                  select c;

			var unprotectedControllers = from c in controllers
			                             where c.BaseType != typeof (AuthorizeBaseController) &&
			                                   c != typeof (AccountController) &&
											   c != typeof (UtilityController)
			                             select c.Name;

			if (unprotectedControllers.Any())
			{
				Assert.Fail("The following controllers should inherit from AuthorizeBaseController: " + string.Join(", ", unprotectedControllers.ToArray()));
			}
		}
	}
}