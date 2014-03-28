using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FailTracker.Web.Data;
using FailTracker.Web.Infrastructure;
using StructureMap;

namespace FailTracker.Web.Controllers
{
	public class HomeController : FailTrackerController
	{
		public HomeController()
		{
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}