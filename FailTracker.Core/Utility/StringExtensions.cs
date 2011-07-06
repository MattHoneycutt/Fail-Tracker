using System.Text.RegularExpressions;

namespace FailTracker.Core.Utility
{
	public static class StringExtensions
	{
		 public static string ToStringWithSpaces(this string input)
		 {
			 return Regex.Replace(
				input,
				"(?<!^)" + // don't match on the first character - never want to place a space here
				"(" +
				"  [A-Z][a-z] |" + // put a space before "Aaaa"
				"  (?<=[a-z])[A-Z] |" + // put a space into "aAAA" before the first capital
				"  (?<![A-Z])[A-Z]$" + // if the last letter is capital, prefix it with a space too
				")",
				" $1",
				RegexOptions.IgnorePatternWhitespace);
		 }
	}
}