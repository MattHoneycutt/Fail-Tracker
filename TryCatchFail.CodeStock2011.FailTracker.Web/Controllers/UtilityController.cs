using System.Web.Mvc;
using Microsoft.Web.Mvc;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Controllers
{
	public class UtilityController : Controller
	{
		public ActionResult Layout()
		{
			return View();
		}

		[HttpGet]
		public ActionResult ResetDatabase()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ResetDatabase(FormCollection form)
		{
			NHibernateBootstrapper.CreateSchema();
			using (var session = NHibernateBootstrapper.GetSession())
			{
				var issues = new[]
				             	{
									Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
									Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
									Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
									Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
				             	};

				foreach (var issue in issues)
				{
					session.Save(issue);
				}

				session.Flush();
			}

			return this.RedirectToAction<IssuesController>(c => c.Dashboard());
		}
	}
}