using FailTracker.Core.Data;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace FailTracker.Web.Infrastructure
{
	public class NHibernateRegistry : Registry
	{
		public NHibernateRegistry()
		{
			For<ISession>().HttpContextScoped().Use(NHibernateBootstrapper.GetSession);
			For(typeof(IRepository<>)).Use(typeof(NHibernateRepository<>));			
		}
	}
}