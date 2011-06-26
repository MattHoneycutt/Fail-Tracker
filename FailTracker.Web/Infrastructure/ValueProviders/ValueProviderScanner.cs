using System;
using System.Web.Mvc;
using FailTracker.Core.Utility;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace FailTracker.Web.Infrastructure.ValueProviders
{
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