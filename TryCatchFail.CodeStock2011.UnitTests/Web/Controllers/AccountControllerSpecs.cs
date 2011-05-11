using System;
using System.Web.Mvc;
using NUnit.Framework;
using SpecsFor;
using TryCatchFail.CodeStock2011.FailTracker.Web.Controllers;
using MvcContrib.TestHelper;

namespace TryCatchFail.CodeStock2011.UnitTests.Web.Controllers
{
	public class AccountControllerSpecs
	{
		public class when_requesting_the_login_page : SpecsFor<AccountController>
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.LogOn();
			}

			[Test]
			public void then_it_returns_the_view()
			{
				_result.AssertViewRendered();
			}
		}
	}	
}