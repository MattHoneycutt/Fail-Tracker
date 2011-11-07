using System;
using System.Linq;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using SpecsFor;

namespace FailTracker.UnitTests.Web.Controllers.Contexts
{
	public abstract class SpecsForWithData<T> : SpecsFor<T> where T : class
	{
		protected User TestUser;
		protected User CreatorUser;
		protected Issue ActiveIssue;
		protected Project TestProject;
		protected Issue CompletedIssue;

		protected override void Given()
		{
			CreateUsers();

			CreateProject();

			CreateIssues();
		}

		private void CreateProject()
		{
			TestProject = Project.Create("Test", CreatorUser);
			TestProject.ID = Guid.NewGuid();

			GetMockFor<IRepository<Project>>()
				.Setup(r => r.Query())
				.Returns((new[] { Project.Create("Blah!", CreatorUser), TestProject }).AsQueryable());
		}

		private void CreateIssues()
		{
			ActiveIssue = Issue.CreateNewIssue(TestProject, "Test Issue", CreatorUser, "This is a test");
			ActiveIssue.ReassignTo(TestUser);
			ActiveIssue.ID = Guid.NewGuid();

			CompletedIssue = Issue.CreateNewIssue(TestProject, "Completed", CreatorUser, "This is a completed issue.");
			CompletedIssue.Complete(TestUser, "Finished!");
			CompletedIssue.ID = Guid.NewGuid();

			GetMockFor<IRepository<Issue>>()
				.Setup(r => r.Query())
				.Returns((new[] { ActiveIssue, CompletedIssue }).AsQueryable());
		}

		private void CreateUsers()
		{
			TestUser = User.CreateNewUser("test@user.com", "blah");
			TestUser.ID = Guid.NewGuid();

			CreatorUser = User.CreateNewUser("some@user.com", "blah");
			CreatorUser.ID = Guid.NewGuid();

			GetMockFor<IRepository<User>>()
				.Setup(s => s.Query())
				.Returns((new[] { CreatorUser, TestUser }).AsQueryable());
		}
	}
}