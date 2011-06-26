using System.Web.Mvc;
using System.Linq;

namespace FailTracker.Web.Infrastructure.ModelMetadata
{
	//TODO: First time this guy grows, break it out and introduce abstractions!
	public class StructureMapModelMetadataProvider : DataAnnotationsModelMetadataProvider
	{
		protected override System.Web.Mvc.ModelMetadata CreateMetadata(System.Collections.Generic.IEnumerable<System.Attribute> attributes, System.Type containerType, System.Func<object> modelAccessor, System.Type modelType, string propertyName)
		{
			var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

			if (attributes.OfType<RenderModeAttribute>().Any())
			{
				switch(attributes.OfType<RenderModeAttribute>().First().RenderMode)
				{
					case RenderMode.None:
						metadata.ShowForDisplay = false;
						metadata.ShowForEdit = false;
						break;
				}
			}

			return metadata;
		}
	}
}