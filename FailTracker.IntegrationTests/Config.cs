using System;
using FailTracker.Web.Infrastructure.Startup;
using NUnit.Framework;
using SpecsFor.Mvc;
using Project = SpecsFor.Mvc.Project;

namespace FailTracker.IntegrationTests
{
	//[SetUpFixture]
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

			config.Use<TestSeedData>();

			config.AuthenticateBeforeEachTestUsing<RegularUserAuthenticator>();

			config.InterceptEmailMessagesOnPort(49999);

			//config.PostOperationDelay(TimeSpan.FromSeconds(1));

			_host = new SpecsForIntegrationHost(config);
			_host.Start();
		}

		[TearDown]
		public void Stop()
		{
			_host.Shutdown();
		}
	}
}