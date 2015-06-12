using StructureMap;

namespace FailTracker.Web.Infrastructure
{
	public static class IoC
	{
		public static IContainer Container { get; set; }

		static IoC()
		{
			Container = new Container();
		}
	}
}