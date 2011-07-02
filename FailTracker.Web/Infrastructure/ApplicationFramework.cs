using FailTracker.Core.Data;
using FailTracker.Web.Infrastructure.Startup;
using FailTracker.Web.Infrastructure.ValueProviders;
using NHibernate;
using StructureMap;

namespace FailTracker.Web.Infrastructure
{
	public static class ApplicationFramework
	{
		public static void Bootstrap()
		{
			ObjectFactory.Initialize(x =>
			{
				x.AddRegistry<DefaultConventionsRegistry>();
				x.AddRegistry<ValueProviderRegistry>();
				x.AddRegistry<TaskRegistry>();
				x.AddRegistry<MvcRegistry>();
				x.AddRegistry<ModelMetadataRegistry>();

				x.For<ISession>().HttpContextScoped().Use(NHibernateBootstrapper.GetSession);
				x.For(typeof(IRepository<>)).Use(typeof(NHibernateRepository<>));
			});
		}

		public static void Start()
		{
			foreach (var task in ObjectFactory.GetAllInstances<IRunAtStartup>())
			{
				task.Execute();
			}
		}
	}
}