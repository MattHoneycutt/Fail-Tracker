using System;
using System.ComponentModel;
using System.Web.Mvc;
using FailTracker.Web.Infrastructure.ModelMetadata.Filters;
using NUnit.Framework;
using SpecsFor;
using Should;

namespace FailTracker.UnitTests.Web.Infrastructure.ModelMetadata
{
	public class PascalCaseToDisplayNameProviderSpecs
	{
		public class when_transforming_simple_property_metadata_with_no_display_name_attribute : given.the_default_state
		{
			protected override void When()
			{
				SUT.TransformMetadata(Metadata, new Attribute[0]);
			}

			[Test]
			public void then_it_sets_the_name()
			{
				Metadata.DisplayName.ShouldEqual("Test Property");
			}
		}

		public class when_transforming_property_with_display_name_attribute : given.the_default_state
		{
			protected override void When()
			{
				SUT.TransformMetadata(Metadata, new Attribute[] {new DisplayNameAttribute("Custom Name")});
			}

			[Test]
			public void then_it_does_not_set_the_display_name()
			{
				Metadata.DisplayName.ShouldBeNull();
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsFor<PascalCaseToDisplayNameProvider>
			{
				protected System.Web.Mvc.ModelMetadata Metadata;

				protected override void Given()
				{
					Metadata = new System.Web.Mvc.ModelMetadata(GetMockFor<ModelMetadataProvider>().Object, this.GetType(), () => null,
															this.GetType(), "TestProperty");
				}
			}
		}
	}
}