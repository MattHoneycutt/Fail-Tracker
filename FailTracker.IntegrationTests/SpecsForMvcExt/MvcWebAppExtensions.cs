using System;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using MvcContrib.TestHelper.Fakes;
using OpenQA.Selenium;
using SpecsFor.Mvc;

namespace FailTracker.IntegrationTests.SpecsForMvcExt
{
	public static class MvcWebAppExtensions
	{
		public static IWebElement FindLinkTo<TController>(this MvcWebApp app, Expression<Action<TController>> action) where TController : Controller
		{
			var helper = new HtmlHelper(new ViewContext { HttpContext = FakeHttpContext.Root() }, new FakeViewDataContainer());

			var url = helper.BuildUrlFromExpression(action);

			var element = app.Browser.FindElement(By.CssSelector("a[href='" + url + "']"));

			return element;
		}

		public static IWebElement ClickButton(this IWebElement element)
		{
			element.SendKeys(Keys.Enter);
			Thread.Sleep(500);

			return element;
		}

		public static string Value(this IWebElement element)
		{
			return element.GetAttribute("value");
		}
	}
}