using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Models.ProjectAdministration;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
	public class ProjectAdministrationController : AuthorizedFailTrackerController
	{
		private readonly IRepository<Project> _projects;

		public ProjectAdministrationController(IRepository<Project> projects)
		{
			_projects = projects;
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
		public ActionResult InviteMember()
		{
			return View();
		}
	}
}