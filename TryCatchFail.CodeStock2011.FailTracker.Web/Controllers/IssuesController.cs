using System;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;
using TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues;
using Microsoft.Web.Mvc;
using NHibernate.Linq;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Controllers
{
	public class IssuesController : Controller
	{
		private readonly ISession _session;

		public IssuesController(ISession session)
		{
			_session = session;
		}

		public ActionResult Index()
		{
			var issues = from i in _session.Query<Issue>()
			             select new IssueViewModel {ID = i.ID, Title = i.Title, AssignedTo = i.AssignedTo};

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
			var issue = new Issue {Title = form.Title, AssignedTo = form.AssignedTo, Body = form.Body};

			_session.Save(issue);

			_session.Flush();

			return this.RedirectToAction(c => c.Index());
		}

		public ActionResult View(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
