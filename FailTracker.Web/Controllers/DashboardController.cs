using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Models;
using FailTracker.Web.Models.Dashboard;

namespace FailTracker.Web.Controllers
{
	public class DashboardController : AuthorizedFailTrackerController
	{
		private readonly IRepository<Project> _projects;

		public DashboardController(IRepository<Project> projects)
		{
			_projects = projects;
		}

		public ActionResult Index()
		{
			var projects = (from p in _projects.Query()
			                select Mapper.Map<Project,ProjectDashboardViewModel>(p)).ToArray();

			return View(new DashboardViewModel{Projects = projects});
		}
	}
}