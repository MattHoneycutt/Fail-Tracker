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
		private readonly IRepository<Issue> _issues;
		private readonly IRepository<User> _users;

		public IssuesController(IRepository<Issue> issues, IRepository<User> users)
		{
			_issues = issues;
			_users = users;
		}

		public ActionResult Dashboard()
		{
			var issues = (from i in _issues.Query()
			              select new IssueViewModel {ID = i.ID, Title = i.Title, AssignedTo = i.AssignedTo.EmailAddress}).ToArray();

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
			var user = _users.Query().Where(u => u.ID == form.AssignedTo).Single();
			var issue = Issue.Create(form.Title, user, form.Body);

			_issues.Save(issue);

			return this.RedirectToAction(c => c.View(issue.ID));
		}

		public ActionResult View(Guid id)
		{
			var model = (from i in _issues.Query()
			             where i.ID == id
			             select new ViewIssueViewModel
			                    	{
										Title = i.Title,
			                    		AssignedTo = i.AssignedTo.EmailAddress,
										Body = i.Body
			                    	}).Single();

			return View(model);
		}
	}
}
