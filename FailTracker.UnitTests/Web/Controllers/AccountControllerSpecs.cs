using System.Web.Mvc;
using NUnit.Framework;
using Should;
using SpecsFor;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.ActionResults;
using FailTracker.Web.Controllers;
using MvcContrib.TestHelper;
using FailTracker.Web.Models.Account;
using System.Linq;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class AccountControllerSpecs
	{
		public class when_requesting_the_login_page : SpecsFor<AccountController>
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.LogOn();
			}

			[Test]
			public void then_it_returns_the_view()
			{
				_result.AssertViewRendered();
			}
		}

		public class when_logging_in_with_a_valid_username : SpecsFor<AccountController>
		{
			private ActionResult _result;

			protected override void Given()
			{
				GetMockFor<IRepository<User>>()
					.Setup(r => r.Query())
					.Returns((new[] {User.CreateNewUser("test@user.com", "12345")}).AsQueryable());
			}

			protected override void When()
			{
				_result = SUT.LogOn(new LogOnForm {EmailAddress = "test@user.com", Password = "12345"});
			}

			[Test]
			public void then_it_should_log_the_user_in()
			{
				_result.ShouldBeType<LogOnResult>()
					.UserName.ShouldEqual("test@user.com");
			}
		}

		public class when_logging_in_with_an_invalid_email_address : SpecsFor<AccountController>
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.LogOn(new LogOnForm {EmailAddress = "not@found.com", Password = "Whatever"});
			}

			[Test]
			public void then_it_rerenders_the_logon_form()
			{
				_result.ShouldBeType<StatusResult>()
					.InnerResult.AssertViewRendered()
					.WithViewData<LogOnForm>().EmailAddress.ShouldEqual("not@found.com");
			}

			[Test]
			public void then_it_sets_the_error_message()
			{
				var status = _result.ShouldBeType<StatusResult>();
				status.Message.ShouldEqual("Invalid username or password.");
				status.Type.ShouldEqual(StatusType.Error);
			}
		}

		public class when_logging_in_with_a_bad_password : SpecsFor<AccountController>
		{
			private ActionResult _result;

			protected override void Given()
			{
				GetMockFor<IRepository<User>>()
					.Setup(r => r.Query())
					.Returns((new[] { User.CreateNewUser("test@user.com", "Good")}).AsQueryable());
			}

			protected override void When()
			{
				_result = SUT.LogOn(new LogOnForm { EmailAddress = "test@user.com", Password = "BAAAD" });
			}

			[Test]
			public void then_it_rerenders_the_logon_form()
			{
				_result.ShouldBeType<StatusResult>()
					.InnerResult.AssertViewRendered()
					.WithViewData<LogOnForm>().EmailAddress.ShouldEqual("test@user.com");
			}

			[Test]
			public void then_it_sets_the_error_message()
			{
				var status = _result.ShouldBeType<StatusResult>();
				status.Message.ShouldEqual("Invalid username or password.");
				status.Type.ShouldEqual(StatusType.Error);
			}
		}

		public class when_logging_out : SpecsFor<AccountController>
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.LogOff();
			}

			[Test]
			public void then_it_returns_a_logout_result()
			{
				_result.ShouldBeType<LogOffResult>();
			}
		}
	}	
}