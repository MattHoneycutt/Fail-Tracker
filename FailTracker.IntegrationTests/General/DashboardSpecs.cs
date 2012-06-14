using FailTracker.Web.Controllers;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;

namespace FailTracker.IntegrationTests.General
{
	public class DashboardSpecs
	{
		public class when_viewing_the_dashboard : SpecsFor<MvcWebApp>
		{
			protected override void When()
			{
				SUT.NavigateTo<DashboardController>(c => c.Index());
			}

			[Test]
			public void then_it_renders_the_test_issues()
			{
				
			}
		}
	}
}