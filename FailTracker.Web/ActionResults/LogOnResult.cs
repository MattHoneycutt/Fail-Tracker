using System.Web.Mvc;
using System.Web.Security;

namespace FailTracker.Web.ActionResults
{
	public class LogOnResult : ActionResult
	{
		public string UserName { get; set; }

		public LogOnResult(string userName)
		{
			UserName = userName;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			FormsAuthentication.SetAuthCookie(UserName, true);
			var redirectResult = new RedirectResult("~/");
			redirectResult.ExecuteResult(context);
		}
	}
}