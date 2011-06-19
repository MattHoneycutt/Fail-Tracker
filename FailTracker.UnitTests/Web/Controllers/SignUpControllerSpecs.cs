using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.ActionResults;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.SignUp;
using Moq;
using NUnit.Framework;
using SpecsFor;
using MvcContrib.TestHelper;
using Should;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class SignUpControllerSpecs
	{
		public class when_requesting_the_signup_form : SpecsFor<SignUpController>
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.SignUp();
			}

			[Test]
			public void then_it_returns_the_signup_view()
			{
				_result.AssertViewRendered();
			}
		}

		public class when_signing_up_with_a_valid_form : SpecsFor<SignUpController>
		{
			private ActionResult _result;
			private SignUpForm _form;

			protected override void When()
			{
				_form = new SignUpForm {EmailAddress = "test@user.com", Password = "12345", PasswordAgain = "12345"};
				_result = SUT.SignUp(_form);
			}

			[Test]
			public void then_it_signs_the_user_in()
			{
				_result.ShouldBeType<LogOnResult>();
			}

			[Test]
			public void then_it_creates_a_new_user()
			{
				GetMockFor<IRepository<User>>()
					.Verify(r => r.Save(It.Is<User>(u => u.EmailAddress == _form.EmailAddress && u.IsThisTheUsersPassword(_form.Password))));
			}
		}
	}	
}