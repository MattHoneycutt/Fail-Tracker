using FailTracker.Web.Infrastructure.Mapping;
using NUnit.Framework;

namespace FailTracker.UnitTests.Web
{
	[SetUpFixture]
	public class AssemblySetup
	{
		[SetUp]
		public void SetUpAutoMapper()
		{
			new MappingBootstrapper().Execute();
		}
	}
}