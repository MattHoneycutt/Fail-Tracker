using System.Web.Mvc;
using System.Web.Routing;

namespace FailTracker.Web.Infrastructure.Startup
{
	public class RouteBootstrapper : IRunAtStartup
	{
		private readonly RouteCollection _routes;

		public RouteBootstrapper(RouteCollection routes)
		{
			_routes = routes;
		}

		public void Execute()
		{
			_routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			_routes.MapRoute("InviteMember",
			                 "{targetProjectID}/Invite",
			                 new {controller = "ProjectAdministration", action = "InviteMember"});

			_routes.MapRoute("LogOn",
			                "LogOn",
			                new {controller = "Authentication", action = "LogOn"});

			_routes.MapRoute("SignUp",
			                "SignUp",
			                new {controller = "SignUp", action = "SignUp"});

			_routes.MapRoute("AddIssue",
			                 "{targetProjectID}/AddIssue",
			                 new {controller = "AddIssue", action = "Index"});

			_routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}
	}
}