using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Models.Backlog;

namespace FailTracker.Web.Controllers
{
	public class BacklogController : AuthorizedFailTrackerController
	{
		private readonly IRepository<Project> _projects;

		public BacklogController(IRepository<Project> projects)
		{
			_projects = projects;
		}

		public ActionResult Index(Guid projectID)
		{
			var model = Mapper.Map<Project, ProjectBacklogViewModel>(_projects.Query().Single(p => p.ID == projectID));

			return View(model);
		}
	}
}