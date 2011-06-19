using System.Web.Mvc;
using System.Web.Routing;

namespace FailTracker.Web.Infrastructure
{
	public static class RouteBootstrapper
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("LogOn",
			                "LogOn",
			                new {controller = "Authentication", action = "LogOn"});

			routes.MapRoute("SignUp",
			                "SignUp",
			                new {controller = "SignUp", action = "SignUp"});

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Issues", action = "Dashboard", id = UrlParameter.Optional } // Parameter defaults
			);
		}
	}
}