using StructureMap.Configuration.DSL;

namespace FailTracker.Web.Infrastructure
{
	public class StandardRegistry : Registry
	{
		public StandardRegistry()
		{
			Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.WithDefaultConventions();
			});
		}
	}
}