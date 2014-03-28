using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace FailTracker.Web.Infrastructure
{
	public class StructureMapFilterProvider : FilterAttributeFilterProvider
	{
		private readonly Func<IContainer> _container;

		public StructureMapFilterProvider(Func<IContainer> container)
		{
			_container = container;
		}

		public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var filters = base.GetFilters(controllerContext, actionDescriptor);

			var container = _container();

			foreach (var filter in filters)
			{
				container.BuildUp(filter.Instance);
				yield return filter;
			}
		}
	}
}