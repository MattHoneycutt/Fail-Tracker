using System.Linq;
using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.ActionResults;
using FailTracker.Web.Models.Authentication;

namespace FailTracker.Web.Controllers
{
	public class AuthenticationController : FailTrackerController
	{
		private readonly IRepository<User> _repository;

		public AuthenticationController(IRepository<User> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public ActionResult LogOn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LogOn(LogOnForm form)
		{
			var user = (from u in _repository.Query()
			            where u.EmailAddress == form.EmailAddress
			            select u).SingleOrDefault();

			if (user == null || !user.IsThisTheUsersPassword(form.Password))
			{
				ModelState.AddModelError("*", "The user name or password provided is incorrect.");
				return View(new LogOnForm {EmailAddress = form.EmailAddress})
						.WithErrorMessage("Invalid username or password.");
			}
			
			return new LogOnResult(form.EmailAddress);
		}

		public ActionResult LogOff()
		{
			return new LogOffResult();
		}
	}
}