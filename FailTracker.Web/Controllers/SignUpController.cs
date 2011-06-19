using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.ActionResults;
using FailTracker.Web.Models.SignUp;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
	public class SignUpController : FailTrackerController
	{
		private IRepository<User> _users;

		public SignUpController(IRepository<User> users)
		{
			_users = users;
		}

		[HttpGet]
		public ActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SignUp(SignUpForm form)
		{
			var user = Core.Domain.User.CreateNewUser(form.EmailAddress, form.Password);

			_users.Save(user);

			return new LogOnResult(user.EmailAddress);
		}
	}
}