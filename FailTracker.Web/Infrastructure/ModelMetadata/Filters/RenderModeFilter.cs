using System;
using System.Collections.Generic;
using System.Linq;
using FailTracker.Web.Infrastructure.ModelMetadata.Attributes;

namespace FailTracker.Web.Infrastructure.ModelMetadata.Filters
{
	public class RenderModeFilter : IModelMetadataFilter
	{
		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
		{
			if (attributes.OfType<RenderModeAttribute>().Any())
			{
				switch (attributes.OfType<RenderModeAttribute>().First().RenderMode)
				{
					case RenderMode.None:
						metadata.ShowForDisplay = false;
						metadata.ShowForEdit = false;
						break;
				}
			}
		}
	}
}