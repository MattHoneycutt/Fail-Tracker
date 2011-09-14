using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Security;
using FailTracker.Web.Models.Profile;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
    public class ProfileController : Controller
    {
    	private readonly CurrentUser _user;
    	private readonly IRepository<User> _users;

    	public ProfileController(CurrentUser user, IRepository<User> users)
    	{
    		_user = user;
    		_users = users;
    	}

    	[HttpGet]
    	public ActionResult MyProfile()
    	{
    		var model = Mapper.Map<User, ProfileViewModel>(_user.Instance);

    		return View(model);
    	}

		[HttpPost]
    	public ActionResult MyProfile(ProfileViewModel model)
		{
			_user.Instance.SetPassword(model.NewPassword);

			_users.Save(_user.Instance);

			return this.RedirectToAction<DashboardController>(c => c.Index());
		}
    }
}
