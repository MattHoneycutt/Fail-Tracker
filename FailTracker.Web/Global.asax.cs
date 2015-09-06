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
using Heroic.Web.IoC;
using StructureMap;
using StructureMap.TypeRules;

namespace FailTracker.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
	    public IContainer Container => StructureMapContainerPerRequestModule.Container;

	    protected void Application_Start()
	    {
		    StructureMapContainerPerRequestModule.PreDisposeContainer += ExecuteEndTasks;

			AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

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

	    public void ExecuteEndTasks(IContainer nestedContainer)
	    {
			foreach (var task in nestedContainer.GetAllInstances<IRunAfterEachRequest>())
			{
				task.Execute();
			}
	    }
    }
}
