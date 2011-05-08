using System;
using System.Linq;
using System.Web.Mvc;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;
using TryCatchFail.CodeStock2011.FailTracker.Web.Models.Issues;
using Microsoft.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Controllers
{
	public class IssuesController : Controller
	{
		private readonly IRepository<Issue> _repository;

		public IssuesController(IRepository<Issue> repository)
		{
			_repository = repository;
		}

		public ActionResult Dashboard()
		{
			var issues = (from i in _repository.Query()
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
			var issue = Issue.Create(form.Title, form.AssignedTo, form.Body);

			_repository.Save(issue);

			return this.RedirectToAction(c => c.View(issue.ID));
		}

		public ActionResult View(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
