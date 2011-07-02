using System.Web.Mvc;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.UnitTests.Web.Controllers.Contexts;
using FailTracker.Web.Controllers;
using FailTracker.Web.Models.ProjectAdministration;
using Moq;
using NUnit.Framework;
using MvcContrib.TestHelper;
using Should;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class ProjectAdministrationControllerSpecs
	{
		public class when_viewing_projects : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.Index();
			}

			[Test]
			public void then_it_returns_the_projects()
			{
				_result.AssertViewRendered().WithViewData<ProjectSummaryViewModel[]>().ShouldNotBeEmpty();
			}
		}

		public class when_adding_a_new_project : given.the_default_state
		{
			private ActionResult _result;
			private AddProjectForm _form;

			protected override void When()
			{
				_form = new AddProjectForm {Name = "Test Project", CurrentUser = TestUser};
				_result = SUT.AddProject(_form);
			}

			[Test]
			public void then_it_adds_the_project()
			{
				GetMockFor<IRepository<Project>>()
					.Verify(r => r.Save(It.Is<Project>(p => p.Name == _form.Name && p.Creator == TestUser)));
			}

			[Test]
			public void then_it_redirects_to_the_dashboard()
			{
				_result.AssertActionRedirect().ToAction<ProjectAdministrationController>(c => c.Index());
			}
		}

		public class when_viewing_the_invite_member_form : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.InviteMember();
			}

			[Test]
			public void then_it_returns_a_view()
			{
				_result.AssertViewRendered();
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsForWithData<ProjectAdministrationController>
			{
			}
		}
	}
}