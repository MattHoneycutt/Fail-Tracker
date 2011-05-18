using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FailTracker.Core.Data;
using FailTracker.Web.Infrastructure;

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

			NHibernateBootstrapper.Bootstrap();
			//TODO: Turn this off once major development is finished. 
			NHibernateBootstrapper.UpdateSchema();
		}
	}

	public static class FilterBootstrapper
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//TODO: Refactor this to use the IoC container and some sort of filter provider method. 

			filters.Add(new HandleErrorAttribute());
		}
	}
}