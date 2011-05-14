using System.Web.Mvc;
using Microsoft.Web.Mvc;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;
using NHibernate.Linq;

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
				(new[] {
				 		Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
				 		Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
				 		Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
				 		Issue.Create("Something doesn't work", "mbhoneycutt@gmail.com", "Body 12345"),
				 	}).ForEach(i => session.Save(i));

				(new[]
				 	{
				 		new User {EmailAddress = "admin@failtracker.foo", Password = "admin"}
				 	}).ForEach(u => session.Save(u));

				session.Flush();
			}

			return this.RedirectToAction<IssuesController>(c => c.Dashboard());
		}
	}
}