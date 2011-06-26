using StructureMap.Configuration.DSL;

namespace FailTracker.Web.Infrastructure
{
	public class DefaultConventionsRegistry : Registry
	{
		public DefaultConventionsRegistry()
		{
			Scan(scan =>
			     	{
			     		scan.TheCallingAssembly();
			     		scan.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.Contains("FailTracker"));
			     		scan.WithDefaultConventions();
			     	});
		}
	}
}