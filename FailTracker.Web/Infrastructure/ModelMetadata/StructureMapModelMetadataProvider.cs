using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FailTracker.Web.Infrastructure.ModelMetadata.Filters;
using NHibernate.Linq;

namespace FailTracker.Web.Infrastructure.ModelMetadata
{
	public class StructureMapModelMetadataProvider : DataAnnotationsModelMetadataProvider
	{
		private readonly IModelMetadataFilter[] _metadataFilters;

		public StructureMapModelMetadataProvider(IModelMetadataFilter[] metadataFilters)
		{
			_metadataFilters = metadataFilters;
		}

		protected override System.Web.Mvc.ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

			_metadataFilters.ForEach(m => m.TransformMetadata(metadata, attributes));

			return metadata;
		}
	}
}