using FailTracker.Core.Utility;
using NUnit.Framework;
using SpecsFor;
using Should;

namespace FailTracker.UnitTests.Core.Utility
{
	public class StringExtensionsSpecs
	{
		public class when_converting_a_string_with_no_pascal_case : given.the_default_state
		{
			private string _result;

			protected override void When()
			{
				_result = "Thisisatest".ToStringWithSpaces();
			}

			[Test]
			public void then_it_returns_the_original_string()
			{
				_result.ShouldEqual("Thisisatest");
			}
		}

		public class when_converting_a_pascal_case_string_with_multiple_words : given.the_default_state
		{
			private string _result;

			protected override void When()
			{
				_result = "ThisHasManyWords".ToStringWithSpaces();
			}

			[Test]
			public void then_it_adds_spaces_between_words()
			{
				_result.ShouldEqual("This Has Many Words");
			}
		}

		public class when_converting_a_pascal_case_string_with_an_acronym_to_multiple_words : given.the_default_state
		{
			private string _result;

			protected override void When()
			{
				_result = "ACountrySuchAsUSAIsAnAcronym".ToStringWithSpaces();
			}

			[Test]
			public void then_it_adds_spaces_between_words()
			{
				_result.ShouldEqual("A Country Such As USA Is An Acronym");
			}
		}

		public static class given
		{
			public abstract class the_default_state : SpecsFor<object>
			{
				
			}
		}
	}
}