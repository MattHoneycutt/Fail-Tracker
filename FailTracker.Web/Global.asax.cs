using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FailTracker.Core.Data;
using FailTracker.Web.Infrastructure;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web
{
	public class MvcApplication : HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			FilterBootstrapper.RegisterGlobalFilters(GlobalFilters.Filters);

			RegisterGlobalFilters(GlobalFilters.Filters);
			
			RouteBootstrapper.RegisterRoutes(RouteTable.Routes);

			MappingBootstrapper.LoadAllMaps();

			NHibernateBootstrapper.Bootstrap();
			//TODO: Turn this off once major development is finished. 
			NHibernateBootstrapper.UpdateSchema();
		}
	}
}