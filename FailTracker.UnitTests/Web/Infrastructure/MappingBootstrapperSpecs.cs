using AutoMapper;
using NUnit.Framework;
using Should;

namespace FailTracker.UnitTests.Web.Infrastructure
{
	[TestFixture]
	public class MappingBootstrapperSpecs
	{
		[Test]
		public void Mappings_should_be_loaded()
		{
			Mapper.GetAllTypeMaps().ShouldNotBeEmpty();
		}

		[Test]
		public void All_mappings_should_be_valid()
		{
			Mapper.AssertConfigurationIsValid();
		}
	}	
}