using System;
using System.Linq;
using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Models.AddIssue;
using FailTracker.Web.Models.Issues;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
	public class AddIssueController : AuthorizedFailTrackerController
	{
		private readonly IRepository<Project> _projects;
		private readonly IRepository<User> _users;
		private readonly IRepository<Issue> _issues;

		public AddIssueController(IRepository<Project> projects, IRepository<User> users, IRepository<Issue> issues)
		{
			_projects = projects;
			_users = users;
			_issues = issues;
		}

		[HttpGet]
		public ActionResult Index(Guid targetProjectID)
		{
			return View(new AddIssueForm {TargetProjectID = targetProjectID});
		}

		[HttpPost]
		public ActionResult Index(AddIssueForm form)
		{
			var project = _projects.Query().Single(p => p.ID == form.TargetProjectID);

			var issue = Issue.CreateNewIssue(project, form.Title, form.CurrentUser, form.Body);
			issue.ChangeTypeTo(form.Type);
			issue.ChangeSizeTo(form.Size);

			if (form.AssignedTo.HasValue)
			{
				issue.ReassignTo(_users.Query().Where(u => u.ID == form.AssignedTo).Single());
			}

			_issues.Save(issue);

			return this.RedirectToAction<IssuesController>(c => c.View(issue.ID));
		}

	}
}