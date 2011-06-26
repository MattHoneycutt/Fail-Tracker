using FailTracker.Web.Infrastructure.Startup;
using StructureMap.Configuration.DSL;

namespace FailTracker.Web.Infrastructure
{
	public class TaskRegistry : Registry
	{
		public TaskRegistry()
		{
			Scan(x =>
			     	{
						x.TheCallingAssembly();
						x.AddAllTypesOf<IRunAtStartup>();
			     	});
		}
	}
}