using Heroic.AutoMapper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FailTracker.Web.AutoMapperConfig), "Configure")]
namespace FailTracker.Web
{
	public static class AutoMapperConfig
	{
		public static void Configure()
		{
			//NOTE: By default, the current project and all referenced projects will be scanned.
			//		You can customize this by passing in a lambda to filter the assemblies by name,
			//		like so:
			//HeroicAutoMapperConfigurator.LoadMapsFromCallerAndReferencedAssemblies(x => x.Name.StartsWith("YourPrefix"));
			HeroicAutoMapperConfigurator.LoadMapsFromCallerAndReferencedAssemblies();
		}
	}
}