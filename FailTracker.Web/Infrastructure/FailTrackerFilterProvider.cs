using System.Collections.Generic;
using System.Web.Mvc;
using FailTracker.Web.Controllers;

namespace FailTracker.Web.Infrastructure
{
	//TODO: Break this up?
	public class FailTrackerFilterProvider : IFilterProvider
	{
		public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var controllerType = controllerContext.Controller.GetType();
			if (!((controllerType == typeof(AccountController) && actionDescriptor.ActionName == "LogOn") ||
				controllerType == typeof(UtilityController)))
			{
				yield return new Filter(new AuthorizeAttribute(), FilterScope.First, 0);				
			}

			yield break;
		}
	}
}