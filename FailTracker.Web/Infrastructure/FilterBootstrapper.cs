using System.Web.Mvc;

namespace FailTracker.Web
{
	public static class FilterBootstrapper
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//TODO: Refactor this to use the IoC container and some sort of filter provider method. 

			filters.Add(new HandleErrorAttribute());
		}
	}
}