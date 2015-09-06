using Heroic.Web.IoC;
using System.Web.Http;
using System.Web.Mvc;
using FailTracker.Web.Infrastructure.Tasks;
using StructureMap;
using StructureMap.Graph;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FailTracker.Web.StructureMapConfig), "Configure")]
namespace FailTracker.Web
{
	public static class StructureMapConfig
	{
		public static void Configure()
		{
			IoC.Container.Configure(cfg =>
			{
				cfg.Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.LookForRegistries();
					scan.WithDefaultConventions();
				});

				cfg.AddRegistry(new ControllerRegistry());
				cfg.AddRegistry(new MvcRegistry());
				cfg.AddRegistry(new ActionFilterRegistry(namespacePrefix: "FailTracker.Web"));

				//TODO: Add other registries and configure your container!
			});

			var resolver = new StructureMapDependencyResolver();
			DependencyResolver.SetResolver(resolver);
			GlobalConfiguration.Configuration.DependencyResolver = resolver;
		}
	}
}