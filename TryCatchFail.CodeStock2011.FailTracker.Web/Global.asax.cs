using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Web.Infrastructure;

namespace TryCatchFail.CodeStock2011.FailTracker.Web
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

			RegisterGlobalFilters(GlobalFilters.Filters);
			
			RouteBootstrapper.RegisterRoutes(RouteTable.Routes);

			NHibernateBootstrapper.Bootstrap();
			//TODO: Turn this off once major development is finished. 
			NHibernateBootstrapper.UpdateSchema();
		}
	}
}