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
		protected Issue TestIssue;
		protected Project TestProject;

		protected override void Given()
		{
			CreateUsers();

			CreateProject();

			CreateIssues();
		}

		private void CreateProject()
		{
			TestProject = Project.Create("Test");
			TestProject.ID = Guid.NewGuid();

			GetMockFor<IRepository<Project>>()
				.Setup(r => r.Query())
				.Returns((new[] { Project.Create("Blah!"), TestProject }).AsQueryable());
		}

		private void CreateIssues()
		{
			TestIssue = Issue.CreateNewIssue(TestProject, "Test Issue", CreatorUser, "This is a test");
			TestIssue.ReassignTo(TestUser);
			TestIssue.ID = Guid.NewGuid();

			GetMockFor<IRepository<Issue>>()
				.Setup(r => r.Query())
				.Returns((new[] { TestIssue }).AsQueryable());
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