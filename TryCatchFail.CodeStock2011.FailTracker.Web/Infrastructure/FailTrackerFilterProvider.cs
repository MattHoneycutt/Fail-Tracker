using System.Collections.Generic;
using System.Web.Mvc;
using TryCatchFail.CodeStock2011.FailTracker.Web.Controllers;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Infrastructure
{
	//TODO: Break this up?
	public class FailTrackerFilterProvider : IFilterProvider
	{
		public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			if (controllerContext.Controller.GetType() != typeof(AccountController) ||
				actionDescriptor.ActionName != "LogOn")
			{
				yield return new Filter(new AuthorizeAttribute(), FilterScope.First, 0);				
			}

			yield break;
		}
	}
}