using FailTracker.Core.Data;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Startup;
using NUnit.Framework;
using SpecsFor.Mvc;
using Project = SpecsFor.Mvc.Project;

namespace FailTracker.IntegrationTests
{
	[SetUpFixture]
	public class Config
	{
		private SpecsForIntegrationHost _host;

		[SetUp]
		public void Start()
		{
			var config = new SpecsForMvcConfig();
			config.UseIISExpress()
				.With(Project.Named("FailTracker.Web"))
				.ApplyWebConfigTransformForConfig("Test");

			config.BuildRoutesUsing(r => new RouteBootstrapper(r).Execute());

			config.BeforeEachTest(() =>
			                      	{
			                      		SetupDatabase();
			                      	});

			_host = new SpecsForIntegrationHost(config);
			_host.Start();
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

		[TearDown]
		public void Stop()
		{
			_host.Shutdown();
		}
	}
}