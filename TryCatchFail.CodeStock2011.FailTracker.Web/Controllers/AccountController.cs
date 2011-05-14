using System.Linq;
using System.Web.Mvc;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;
using TryCatchFail.CodeStock2011.FailTracker.Web.ActionResults;
using TryCatchFail.CodeStock2011.FailTracker.Web.Models.Account;
using Microsoft.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IRepository<User> _repository;

		public AccountController(IRepository<User> repository)
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
			            where u.EmailAddress == form.EmailAddress &&
			                  u.Password == form.Password
			            select u).SingleOrDefault();

			if (user == null)
			{
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