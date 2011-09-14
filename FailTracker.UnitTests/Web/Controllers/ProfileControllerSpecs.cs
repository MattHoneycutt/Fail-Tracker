using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Controllers;
using FailTracker.Web.Infrastructure.Security;
using FailTracker.Web.Models.Profile;
using NUnit.Framework;
using SpecsFor;
using MvcContrib.TestHelper;
using Should;
using StructureMap;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class ProfileControllerSpecs
	{
		public class when_viewing_my_profile : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.MyProfile();
			}

			[Test]
			public void then_it_returns_my_profile_info()
			{
				_result.AssertViewRendered()
					.WithViewData<ProfileViewModel>()
					.EmailAddress.ShouldEqual(TestUser.Instance.EmailAddress);
			}
		}

		public class when_saving_a_new_password : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.MyProfile(new ProfileViewModel{NewPassword = "testing123"});
			}

			[Test]
			public void then_it_redirects_to_the_dashboard()
			{
				_result.AssertActionRedirect().ToAction<DashboardController>(c => c.Index());
			}

			[Test]
			public void then_it_changes_the_users_password()
			{
				TestUser.Instance.IsThisTheUsersPassword("testing123").ShouldBeTrue();
			}

			[Test]
			public void then_it_saves_the_user_to_the_repo()
			{
				GetMockFor<IRepository<User>>()
					.Verify(r => r.Save(TestUser.Instance));
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsFor<ProfileController>
			{
				protected CurrentUser TestUser;

				protected override void ConfigureContainer(IContainer container)
				{
					base.ConfigureContainer(container);

					TestUser = new CurrentUser(User.CreateNewUser("test@user.com", "blah"));
					container.Configure(cfg => cfg.For<CurrentUser>().Use(TestUser));
				}

				protected override void Given()
				{
					
				}
			}
		}
	}
}