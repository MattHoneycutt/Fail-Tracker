using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Models.ReactivateIssue;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
	public class ReactivateIssueController : AuthorizedFailTrackerController
	{
		private readonly IRepository<Issue> _repository;

		public ReactivateIssueController(IRepository<Issue> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public ActionResult Reactivate(Guid targetIssueID)
		{
			var issue = _repository.Query().Single(i => i.ID == targetIssueID);

			var form = Mapper.Map<Issue, ReactivateIssueForm>(issue);

			return View(form);
		}

		public ActionResult Reactivate(ReactivateIssueForm form)
		{
			var issue = _repository.Query().Single(i => i.ID == form.ID);

			issue.Reactivate(form.CurrentUser, form.Comments);

			return this.RedirectToAction<IssuesController>(c => c.View(form.ID));
		}
	}
}