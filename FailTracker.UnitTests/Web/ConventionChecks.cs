using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FailTracker.Core.Utility;
using FailTracker.Web.Controllers;
using NUnit.Framework;

namespace FailTracker.UnitTests.Web
{
	[TestFixture]
	public class ConventionChecks
	{
		private static IEnumerable<Type> GetControllers()
		{
			return from c in typeof(AuthorizedFailTrackerController).Assembly.GetExportedTypes()
				   where !c.IsAbstract &&
						 typeof(Controller).IsAssignableFrom(c)
				   select c;
		}

		[Test]
		public void All_controllers_inherit_from_FailTrackerController()
		{
			var controllers = GetControllers();

			var controllersWithWrongBase = from c in controllers
			                               where !c.CanBeCastTo<FailTrackerController>()
			                               select c.Name;

			if (controllersWithWrongBase.Any())
			{
				Assert.Fail("The following controllers have the wrong base: " +
				            string.Join(", ", controllersWithWrongBase.ToArray()));
			}
		}

		[Test]
		public void All_controllers_except_account_controller_inherit_from_AuthorizedFailTrackerController()
		{
			var controllers = GetControllers();

			var unprotectedControllers = from c in controllers
			                             where c.BaseType != typeof (AuthorizedFailTrackerController) &&
			                                   c != typeof (AuthenticationController) &&
											   c != typeof (UtilityController)
			                             select c.Name;

			if (unprotectedControllers.Any())
			{
				Assert.Fail("The following controllers should inherit from AuthorizedFailTrackerController: " + string.Join(", ", unprotectedControllers.ToArray()));
			}
		}
	}
}