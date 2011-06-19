using System.Web.Mvc;
using System.Linq;

namespace FailTracker.Web.Controllers
{
	public abstract class FailTrackerController : Controller
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (ActionNeedsServerSideValidation(filterContext))
			{
				if (!filterContext.Controller.ViewData.ModelState.IsValid)
				{
					HandleModelValidationFailure(filterContext);
				}
			}

			base.OnActionExecuting(filterContext);
		}

		private static void HandleModelValidationFailure(ActionExecutingContext filterContext)
		{
			var result = new ViewResult { ViewData = new ViewDataDictionary(filterContext.Controller.ViewData) { Model = filterContext.ActionParameters.Values.Single() }};
			filterContext.Result = result;
		}

		private static bool ActionNeedsServerSideValidation(ActionExecutingContext filterContext)
		{
			return filterContext.ActionDescriptor.GetParameters().Any(p => p.ParameterType.Name.Contains("Form"));
		}
	}
}