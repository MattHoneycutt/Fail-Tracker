using System.Web.Mvc;
using System.Web.Routing;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Infrastructure
{
	public static class RouteBootstrapper
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Issues", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}
	}
}