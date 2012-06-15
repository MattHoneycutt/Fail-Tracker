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
using FailTracker.Web.ActionResults;

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
				_result.AssertViewRendered().WithViewData<ProjectListViewModel>().Summaries.ShouldNotBeEmpty();
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
				_result = SUT.InviteMember(TestProject.ID);
			}

			[Test]
			public void then_it_returns_a_view_with_the_project_id()
			{
				_result.AssertViewRendered().WithViewData<InviteMemberForm>().ProjectID.ShouldEqual(TestProject.ID);
			}
		}

		public class when_inviting_an_existing_member : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.InviteMember(new InviteMemberForm { EmailAddress = TestUser.EmailAddress, ProjectID = TestProject.ID});
			}

			[Test]
			public void then_it_adds_the_user_to_the_project()
			{
				TestProject.Members.ShouldContain(TestUser);
			}

			[Test]
			public void then_it_redirects_back_the_form_with_a_success_message()
			{
				var result = _result.ShouldBeType<StatusResult>();
				result.Type.ShouldEqual(StatusType.Success);
				result.Message.ShouldNotBeNull();
				result.InnerResult.AssertActionRedirect().ToAction<ProjectAdministrationController>(
					c => c.InviteMember(TestProject.ID));
			}
		}

		//TODO: This feature is pretty crappy, we should really E-mail the user and ask them to join...
		public class when_the_member_does_not_exist : given.the_default_state
		{
			private ActionResult _result;

			protected override void When()
			{
				_result = SUT.InviteMember(new InviteMemberForm { EmailAddress = "unknown@user.com", ProjectID = TestProject.ID });
			}

			[Test]
			public void then_it_redisplays_the_form_with_an_error_message()
			{
				var result = _result.ShouldBeType<StatusResult>();
				result.InnerResult.AssertViewRendered();
				result.Type.ShouldEqual(StatusType.Error);
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