using System;
using System.Web.Mvc;

namespace FailTracker.Web.Infrastructure.ModelMetadata
{
	public class RenderAttribute : Attribute, IMetadataAware
	{
		public bool ShowForEdit { get; set; }
		public bool ShowForDisplay { get; set; }

		public RenderAttribute()
		{
			ShowForEdit = true;
			ShowForDisplay = true;
		}

		public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
		{
			metadata.ShowForEdit = ShowForEdit;
			metadata.ShowForDisplay = ShowForDisplay;
		}
	}
}