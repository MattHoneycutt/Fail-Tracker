using System;
using System.Collections.Generic;

namespace FailTracker.Web.Infrastructure.ModelMetadata.Filters
{
	public class UserIDDropDownByNameFilter : IModelMetadataFilter
	{
		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes)
		{
			if (!string.IsNullOrEmpty(metadata.PropertyName) &&
				string.IsNullOrEmpty(metadata.DataTypeName) &&
				metadata.PropertyName.ToLower().Contains("assignedto"))
			{
				metadata.DataTypeName = "UserID";
			}
		}
	}
}