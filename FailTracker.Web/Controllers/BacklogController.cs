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

		public ActionResult Active(Guid projectID)
		{
			var model = Mapper.Map<Project, BacklogStoriesViewModel>(_projects.Query().Single(p => p.ID == projectID));

			return View(model);
		}

		public ActionResult Completed(Guid projectID)
		{
			var model = Mapper.Map<Project, CompletedStoriesViewModel>(_projects.Query().Single(p => p.ID == projectID));

			return View(model);
		}
	}
}