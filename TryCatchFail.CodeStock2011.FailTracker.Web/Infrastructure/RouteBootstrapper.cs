using System.Web.Mvc;
using System.Web.Routing;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Infrastructure
{
	public static class RouteBootstrapper
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("LogOn",
			                "LogOn",
			                new {controller = "Account", action = "LogOn"});

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Issues", action = "Dashboard", id = UrlParameter.Optional } // Parameter defaults
			);
		}
	}
}