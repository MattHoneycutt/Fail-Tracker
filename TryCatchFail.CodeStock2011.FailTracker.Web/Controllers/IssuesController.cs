using System;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;
using TryCatchFail.CodeStock2011.FailTracker.Core.Features.Dashboard;
using TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues;
using Microsoft.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Controllers
{
	public class IssuesController : Controller
	{
		private readonly ISession _session;
		private readonly IProvideQueries _queries;

		public IssuesController(ISession session, IProvideQueries queries)
		{
			_session = session;
			_queries = queries;
		}

		public ActionResult Index()
		{
			var issues = (from i in _queries.PrepareQuery<GetIssueHeadersQuery>().Run(new GetIssueHeadersQuery.Options())
			              select new IssueViewModel {ID = i.ID, Title = i.Title, AssignedTo = i.AssignedTo}).ToArray();

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

			return this.RedirectToAction(c => c.View(issue.ID));
		}

		public ActionResult View(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
