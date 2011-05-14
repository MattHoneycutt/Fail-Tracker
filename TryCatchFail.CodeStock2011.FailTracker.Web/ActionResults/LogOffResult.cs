using System.Web.Mvc;
using System.Web.Security;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.ActionResults
{
	public class LogOffResult : ActionResult
	{
		public override void ExecuteResult(ControllerContext context)
		{
			FormsAuthentication.SignOut();
			var redirectResult = new RedirectResult("~/");
			redirectResult.ExecuteResult(context);
		}
	}
}