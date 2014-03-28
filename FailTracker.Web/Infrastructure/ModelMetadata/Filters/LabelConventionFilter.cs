using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FailTracker.Web.Infrastructure.ModelMetadata.Filters
{
	public class LabelConventionFilter : IModelMetadataFilter
	{
		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes)
		{
			if (!string.IsNullOrEmpty(metadata.PropertyName) &&
				string.IsNullOrEmpty(metadata.DisplayName))
			{
				metadata.DisplayName = GetStringWithSpaces(metadata.PropertyName);
			}
		}

		private string GetStringWithSpaces(string input)
		{
			return Regex.Replace(
			   input,
			   "(?<!^)" +
			   "(" +
			   "  [A-Z][a-z] |" +
			   "  (?<=[a-z])[A-Z] |" +
			   "  (?<![A-Z])[A-Z]$" +
			   ")",
			   " $1",
			   RegexOptions.IgnorePatternWhitespace);
		}
	}
}