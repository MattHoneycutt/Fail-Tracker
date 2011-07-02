using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FailTracker.Core.Utility;

namespace FailTracker.Web.Infrastructure.ModelMetadata.Filters
{
	public class PascalCaseToDisplayNameProvider : IModelMetadataFilter
	{
		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
		{
			if (!string.IsNullOrEmpty(metadata.PropertyName) && !attributes.OfType<DisplayNameAttribute>().Any())
			{
				metadata.DisplayName = metadata.PropertyName.ToStringWithSpaces();
			}
		}
	}
}