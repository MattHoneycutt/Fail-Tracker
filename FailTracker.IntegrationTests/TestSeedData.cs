using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using SpecsFor.Mvc;

namespace FailTracker.IntegrationTests
{
	public class TestSeedData : SpecsForMvcConfig
	{
		public TestSeedData()
		{
			BeforeEachTest(SetupDatabase);
		}

		private void SetupDatabase()
		{
			NHibernateBootstrapper.Bootstrap();
			NHibernateBootstrapper.CreateSchema();

			using (var session = NHibernateBootstrapper.GetSession())
			{
				var user = User.CreateNewUser("test@user.com", "TestPassword01");
				session.Save(user);

				session.Flush();
			}
		}
	}
}