using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FailTracker.Web.Data;
using FailTracker.Web.Domain;
using FailTracker.Web.Filters;
using FailTracker.Web.Infrastructure;
using FailTracker.Web.Infrastructure.Alerts;
using FailTracker.Web.Models.Issue;

namespace FailTracker.Web.Controllers
{
	public class IssueController : FailTrackerController
	{
		private readonly ApplicationDbContext _context;
		private readonly ICurrentUser _currentUser;

		public IssueController(ApplicationDbContext context, 
			ICurrentUser currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		[ChildActionOnly]
		public ActionResult YourIssuesWidget()
		{
			var models = _context.Issues.Where(i => i.AssignedTo.Id == _currentUser.User.Id)
				.Project().To<IssueSummaryViewModel>();

			return PartialView(models.ToArray());
		}

		[ChildActionOnly]
		public ActionResult CreatedByYouWidget()
		{
			var models = _context.Issues.Where(i => i.Creator.Id == _currentUser.User.Id)
				.Project().To<IssueSummaryViewModel>();

			return PartialView(models.ToArray());
		}

		[ChildActionOnly]
		public ActionResult AssignmentStatsWidget()
		{
			var stats = _context.Users.Project().To<AssignmentStatsViewModel>();

			return PartialView(stats.ToArray());
		}

		public ActionResult New()
		{
			var form = new NewIssueForm();
			return View(form);
		}

		[HttpPost, ValidateAntiForgeryToken, Log("Created issue")]
		public ActionResult New(NewIssueForm form)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			var assignedToUser = _context.Users.Single(u => u.Id == form.AssignedToUserID);

			_context.Issues.Add(new Issue(_currentUser.User, assignedToUser, form.IssueType, form.Subject, form.Body));

			_context.SaveChanges();

			return RedirectToAction<HomeController>(c => c.Index())
				.WithSuccess("Issue created!");
		}

		[Log("Viewed issue {id}")]
		public ActionResult View(int id)
		{
			var model = _context.Issues
				.Project().To<IssueDetailsViewModel>()
				.SingleOrDefault(i => i.IssueID == id);

			if (model == null)
			{
				return RedirectToAction<HomeController>(c => c.Index())
					.WithError("Unable to find the issue.  Maybe it was deleted?");
			}

			return View(model);
		}

		[HttpPost, Log("Saving changes")]
		public ActionResult Edit(EditIssueForm form)
		{
			if (!ModelState.IsValid)
			{
				return JsonValidationError();
			}

			var issue = _context.Issues.SingleOrDefault(i => i.IssueID == form.IssueID);

			if (issue == null)
			{
				return JsonError("Cannot find the issue specified.");
			}

			var assignedToUser = _context.Users.Single(u => u.UserName == form.AssignedToUserName);

			issue.Subject = form.Subject;
			issue.AssignedTo = assignedToUser;
			issue.Body = form.Body;
			issue.IssueType = form.IssueType;

			return JsonSuccess(form);
		}

		[HttpPost, ValidateAntiForgeryToken, Log("Deleted issue {id}")]
		public ActionResult Delete(int id)
		{
			var issue = _context.Issues.Find(id);

			if (issue == null)
			{
				return RedirectToAction<HomeController>(c => c.Index())
					.WithError("Unable to find the issue.  Maybe it was deleted?");
			}

			_context.Issues.Remove(issue);

			_context.SaveChanges();

			return RedirectToAction<HomeController>(c => c.Index())
				.WithSuccess("Issue deleted!");
		}
	}
}