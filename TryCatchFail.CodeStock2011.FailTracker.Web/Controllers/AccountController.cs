using System.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public ActionResult LogOn()
		{
			return View();
		}
	}
}