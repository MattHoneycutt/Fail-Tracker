using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.ActionResults;
using FailTracker.Web.Models.ProjectAdministration;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
	public class ProjectAdministrationController : AuthorizedFailTrackerController
	{
		private readonly IRepository<Project> _projects;
		private IRepository<User> _users;

		public ProjectAdministrationController(IRepository<Project> projects, IRepository<User> users)
		{
			_projects = projects;
			_users = users;
		}

		public ActionResult Index()
		{
			var summaries = (from p in _projects.Query()
			                 select Mapper.Map<Project, ProjectSummaryViewModel>(p)).ToArray();

			return View(summaries);
		}

		[HttpGet]
		public ActionResult AddProject()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddProject(AddProjectForm form)
		{
			var project = Project.Create(form.Name, form.CurrentUser);

			_projects.Save(project);

			return this.RedirectToAction(c => c.Index());
		}

		[HttpGet]
		public ActionResult InviteMember(Guid targetProjectID)
		{
			return View(new InviteMemberForm {ProjectID = targetProjectID});
		}

		[HttpPost]
		public ActionResult InviteMember(InviteMemberForm form)
		{
			var targetUser = _users.Query().FirstOrDefault(u => u.EmailAddress == form.EmailAddress);

			if (targetUser == null)
			{
				return
					View().WithErrorMessage(
						"Sorry, that user does not have a Fail Tracker account.  Ask them to create an account first!");
			}

			var project = _projects.Query().Single(p => p.ID == form.ProjectID);

			project.AddMember(targetUser);

			return this.RedirectToAction(c => c.InviteMember(form.ProjectID))
				.WithSuccessMessage("The user has been added to the project.");
		}
	}
}