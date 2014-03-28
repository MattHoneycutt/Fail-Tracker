using System;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace FailTracker.Web.Infrastructure
{
	public class ControllerConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.CanBeCastTo(typeof (Controller)) && !type.IsAbstract)
			{
				registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
			}
		}
	}
}