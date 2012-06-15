using System;
using System.Linq;
using System.Net.Mail;
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

			return View(new ProjectListViewModel {Summaries = summaries});
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

			//TODO: Should this require the current user?  Is the domain rule "only owner of a project can add new members"? 
			//TODO: We still want to prevent users who don't have access to an issue/project from even seeing it in the first place...
			project.AddMember(targetUser);

			//TODO: Move this out of the controller, it is horribly hacky.
			try
			{
				using (var client = new SmtpClient())
				{
					client.Send("noreply@failtracker.com", targetUser.EmailAddress, "You've been added to a project in Fail Tracker!", "Welcome to the " + project.Name + " project!");
				}
			}
			//If mail sending fails, it doesn't really matter...
			catch
			{
				
			}

			return this.RedirectToAction(c => c.InviteMember(form.ProjectID))
				.WithSuccessMessage("The user has been added to the project.");
		}
	}
}