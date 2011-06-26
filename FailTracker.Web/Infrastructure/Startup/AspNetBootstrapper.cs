using System.Web.Mvc;
using FailTracker.Web.Infrastructure.ModelMetadata;
using StructureMap;

namespace FailTracker.Web.Infrastructure.Startup
{
	public class AspNetBootstrapper : IRunAtStartup
	{
		private readonly IContainer _container;

		public AspNetBootstrapper(IContainer container)
		{
			_container = container;
		}

		public void Execute()
		{
			//TODO: Refactor this to use the IoC container and some sort of filter provider method. 
			GlobalFilters.Filters.Add(new HandleErrorAttribute());

			AreaRegistration.RegisterAllAreas();

			DependencyResolver.SetResolver(new StructureMapDependencyResolver(_container));

			ModelMetadataProviders.Current = new StructureMapModelMetadataProvider();
		}
	}
}