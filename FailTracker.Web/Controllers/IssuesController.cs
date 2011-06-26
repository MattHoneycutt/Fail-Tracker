using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Models.Issues;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
	public class IssuesController : AuthorizedFailTrackerController
	{
		private readonly IRepository<Issue> _issues;
		private readonly IRepository<User> _users;
		private readonly IRepository<Project> _projects;

		public IssuesController(IRepository<Issue> issues, IRepository<User> users, IRepository<Project> projects)
		{
			_issues = issues;
			_users = users;
			_projects = projects;
		}

		public ActionResult View(Guid id)
		{
			var model = Mapper.Map<Issue, IssueDetailsViewModel>(_issues.Query().Single(i => i.ID == id));

			return View(model);
		}

		[HttpGet]
		public ActionResult Edit(Guid id)
		{
			var editForm = Mapper.Map<Issue, EditIssueForm>(_issues.Query().Single(i => i.ID == id));

			return View(editForm);
		}

		[HttpPost]
		public ActionResult Edit(EditIssueForm form)
		{
			var issue = _issues.Query().Single(i => i.ID == form.ID);

			var assignedTo = _users.Query().SingleOrDefault(u => u.ID == form.AssignedToID);

			issue.BeginEdit(form.CurrentUser, form.Comments);
			issue.ReassignTo(assignedTo);
			issue.ChangeSizeTo(form.Size);
			issue.ChangeTypeTo(form.Type);
			issue.ChangeTitleTo(form.Title);
			issue.ChangeDescriptionTo(form.Description);

			return this.RedirectToAction(c => c.View(form.ID));
		}

		public ActionResult Details(Guid id)
		{
			var model = Mapper.Map<Issue, IssueDetailsViewModel>(_issues.Query().Single(i => i.ID == id));
			
			return View(model);
		}
	}
}
