using System.Web.Mvc;
using NHibernate;
using StructureMap;
using FailTracker.Core.Data;

namespace FailTracker.Web.Infrastructure.DependencyResolution
{
	public static class IoC
	{
		public static IContainer Initialize()
		{
			ObjectFactory.Initialize(x =>
						{
							x.Scan(scan =>
									{
										scan.TheCallingAssembly();
										scan.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.Contains("FailTracker"));
										scan.WithDefaultConventions();
									});

							x.For<ISession>().Use(NHibernateBootstrapper.GetSession);
							x.For(typeof (IRepository<>)).Use(typeof (NHibernateRepository<>));
							x.For<IFilterProvider>().Use<FailTrackerFilterProvider>();
						});

			return ObjectFactory.Container;
		}
	}
}