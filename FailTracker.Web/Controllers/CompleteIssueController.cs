using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Models.CompleteIssue;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
    public class CompleteIssueController : AuthorizedFailTrackerController
    {
    	private readonly IRepository<Issue> _repository;

    	public CompleteIssueController(IRepository<Issue> repository)
    	{
    		_repository = repository;
    	}

		[HttpGet]
		public ActionResult Complete(Guid targetIssueID)
    	{
    		var issue = _repository.Query().Single(i => i.ID == targetIssueID);

    		var form = Mapper.Map<Issue, CompleteIssueForm>(issue);
    		
			return View(form);
    	}

		[HttpPost]
    	public ActionResult Complete(CompleteIssueForm form)
    	{
    		var issue = _repository.Query().Single(i => i.ID == form.ID);

    		issue.Complete(form.CurrentUser, form.Comments);

    		return this.RedirectToAction<IssuesController>(c => c.View(form.ID));
    	}
    }
}
