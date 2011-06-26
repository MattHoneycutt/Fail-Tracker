using FailTracker.Core.Data;

namespace FailTracker.Web.Infrastructure.Startup
{
	public class NHibernateBootstrapperWrapper : IRunAtStartup
	{
		public void Execute()
		{
			NHibernateBootstrapper.Bootstrap();
			//TODO: Turn this off once major development is finished. 
			NHibernateBootstrapper.UpdateSchema();
		}
	}
}