using StructureMap.Configuration.DSL;
using StructureMap.Graph;

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