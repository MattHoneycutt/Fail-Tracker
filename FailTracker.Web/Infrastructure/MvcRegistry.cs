using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap.Configuration.DSL;

namespace FailTracker.Web.Infrastructure
{
	public class MvcRegistry : Registry
	{
		public MvcRegistry()
		{
			For<BundleCollection>().Use(BundleTable.Bundles);
			For<RouteCollection>().Use(RouteTable.Routes);
			For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
			For<HttpSessionStateBase>()
				.Use(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
			For<HttpContextBase>()
				.Use(() => new HttpContextWrapper(HttpContext.Current));
			For<HttpServerUtilityBase>()
				.Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
		}
	}
}