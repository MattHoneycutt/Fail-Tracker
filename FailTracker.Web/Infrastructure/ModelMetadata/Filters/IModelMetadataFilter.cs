using System;
using System.Collections.Generic;

namespace FailTracker.Web.Infrastructure.ModelMetadata.Filters
{
	public interface IModelMetadataFilter
	{
		void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes);
	}
}