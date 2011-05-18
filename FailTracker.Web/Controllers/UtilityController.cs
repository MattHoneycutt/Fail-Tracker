using System.Web.Mvc;
using Microsoft.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using NHibernate.Linq;

namespace FailTracker.Web.Controllers
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
				var users = new[]
				            	{
				            		Core.Domain.User.CreateNewUser("admin@failtracker.com", "admin"),
				            		Core.Domain.User.CreateNewUser("user@failtracker.com", "user")
				            	};
				users.ForEach(u => session.Save(u));
				
				(new[] {
				 		Issue.CreateNewStory("I want this feature!", users[0], "Body 12345"),
				 		Issue.CreateNewStory("I want this feature!", users[1], "Body 12345"),
				 		Issue.CreateNewStory("I want this feature!", users[0], "Body 12345"),
				 		Issue.CreateNewStory("I want this feature!", users[1], "Body 12345"),
				 		Issue.CreateNewBug("Something doesn't work", users[0], "Body 12345"),
				 		Issue.CreateNewBug("Something doesn't work", users[1], "Body 12345"),
				 		Issue.CreateNewBug("Something doesn't work", users[0], "Body 12345"),
				 		Issue.CreateNewBug("Something doesn't work", users[1], "Body 12345"),
				 	}).ForEach(i => session.Save(i));

				session.Flush();
			}

			return this.RedirectToAction<IssuesController>(c => c.Dashboard());
		}
	}
}