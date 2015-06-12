using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FailTracker.Web.Data;
using FailTracker.Web.Infrastructure;
using FailTracker.Web.Infrastructure.ModelMetadata;
using FailTracker.Web.Infrastructure.Tasks;
using FailTracker.Web.Migrations;
using StructureMap;
using StructureMap.TypeRules;

namespace FailTracker.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
	    public IContainer Container
	    {
		    get
		    {
			    return (IContainer) HttpContext.Current.Items["_Container"];
		    }
		    set
		    {
			    HttpContext.Current.Items["_Container"] = value;
		    }
	    }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

	        DependencyResolver.SetResolver(
				new StructureMapDependencyResolver(() => Container ?? IoC.Container));

			IoC.Container.Configure(cfg =>
	        {
		        cfg.AddRegistry(new StandardRegistry());
				cfg.AddRegistry(new ControllerRegistry());
		        cfg.AddRegistry(new ActionFilterRegistry(
					() => Container ?? IoC.Container));
				cfg.AddRegistry(new MvcRegistry());
				cfg.AddRegistry(new TaskRegistry());
				cfg.AddRegistry(new ModelMetadataRegistry());
			});

	        using (var container = IoC.Container.GetNestedContainer())
	        {
		        foreach (var task in container.GetAllInstances<IRunAtInit>())
		        {
			        task.Execute();
		        }

		        foreach (var task in container.GetAllInstances<IRunAtStartup>())
		        {
			        task.Execute();
		        }
	        }
        }

	    public void Application_BeginRequest()
	    {
		    Container = IoC.Container.GetNestedContainer();

		    foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
		    {
			    task.Execute();
		    }
	    }

	    public void Application_Error()
	    {
		    foreach (var task in Container.GetAllInstances<IRunOnError>())
		    {
			    task.Execute();
		    }
	    }

	    public void Application_EndRequest()
	    {
		    try
		    {
			    foreach (var task in 
					Container.GetAllInstances<IRunAfterEachRequest>())
			    {
				    task.Execute();
			    }
		    }
		    finally
		    {
				Container.Dispose();
				Container = null;
			}
	    }
    }
}
