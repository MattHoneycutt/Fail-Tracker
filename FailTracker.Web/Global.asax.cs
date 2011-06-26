using System.Web;
using FailTracker.Web.Infrastructure;

namespace FailTracker.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			ApplicationFramework.Bootstrap();
			ApplicationFramework.Start(this);
		}
	}
}