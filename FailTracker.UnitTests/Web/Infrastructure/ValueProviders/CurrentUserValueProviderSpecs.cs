using System.Security.Principal;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.ValueProviders;
using NUnit.Framework;
using SpecsFor;
using Should;
using System.Linq;

namespace FailTracker.UnitTests.Web.Infrastructure.ValueProviders
{
	public class CurrentUserValueProviderSpecs
	{
		public class when_checking_prefix_for_current_user : given.the_default_state
		{
			private bool _result;

			protected override void When()
			{
				_result = SUT.ContainsPrefix("CurrentUser");
			}

			[Test]
			public void then_it_returns_true()
			{
				_result.ShouldBeTrue();
			}
		}

		public class when_checking_prefix_for_another_property : given.the_default_state
		{
			private bool _result;

			protected override void When()
			{
				_result = SUT.ContainsPrefix("SomeProp");
			}

			[Test]
			public void then_it_returns_false()
			{
				_result.ShouldBeFalse();
			}
		}

		public class when_getting_value_for_current_user : given.the_default_state
		{
			private object _result;

			protected override void When()
			{
				_result = SUT.GetValue("CurrentUser").RawValue;
			}

			[Test]
			public void then_it_returns_the_current_user()
			{
				_result.ShouldEqual(CurrentUser);
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsFor<CurrentUserValueProvider>
			{
				protected User CurrentUser;

				protected override void Given()
				{
					CurrentUser = User.CreateNewUser("test@user.com", "blah");

					GetMockFor<IIdentity>()
						.SetupGet(i => i.Name).Returns(CurrentUser.EmailAddress);

					GetMockFor<IRepository<User>>()
						.Setup(u => u.Query())
						.Returns((new[] {User.CreateNewUser("test2@user.com", "blah"), CurrentUser}).AsQueryable());
				}
			}
		}
	}
}