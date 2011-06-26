using System;
using System.Web.Mvc;
using FailTracker.Web.Infrastructure.ValueProviders;
using NHibernate;
using StructureMap;
using FailTracker.Core.Data;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using FailTracker.Core.Utility;

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

							x.Scan(scanner =>
							       	{
							       		scanner.TheCallingAssembly();
										scanner.Convention<ValueProviderScanner>();
							       	});

							x.For<ISession>().HttpContextScoped().Use(NHibernateBootstrapper.GetSession);
							x.For(typeof (IRepository<>)).Use(typeof (NHibernateRepository<>));
						});

			return ObjectFactory.Container;
		}
	}

	public class ValueProviderScanner : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.CanBeCastTo<IValueProvider>())
			{
				var factoryType = typeof(StructureMapValueProviderFactory<>).MakeGenericType(type);
				registry.For<ValueProviderFactory>().Use(c => (ValueProviderFactory)c.GetInstance(factoryType));
			}
		}
	}
}