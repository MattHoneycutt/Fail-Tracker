using System.Web.Mvc;
using TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues;
using Microsoft.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Controllers
{
	public class IssuesController : Controller
	{
		public ActionResult Index()
		{
			var issues = new[]
			             	{
			             		new IssueViewModel {ID = 1, Title = "Test 1", AssignedTo = "mbhoneycutt@gmail.com"},
			             		new IssueViewModel {ID = 2, Title = "Test 2", AssignedTo = "mbhoneycutt@gmail.com"},
			             		new IssueViewModel {ID = 3, Title = "Test 3", AssignedTo = "mbhoneycutt@gmail.com"},
			             	};
			return View(issues);
		}

		[HttpGet]
		public ActionResult AddIssue()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddIssue(AddIssueForm form)
		{
			//TODO: Save!

			return this.RedirectToAction(c => c.Index());
		}
	}
}
