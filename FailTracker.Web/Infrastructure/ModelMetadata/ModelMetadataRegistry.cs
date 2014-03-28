using System.Web.Mvc;
using StructureMap.Configuration.DSL;

namespace FailTracker.Web.Infrastructure.ModelMetadata
{
	public class ModelMetadataRegistry : Registry
	{
		public ModelMetadataRegistry()
		{
			For<ModelMetadataProvider>().Use<ExtensibleModelMetadataProvider>();

			Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.AddAllTypesOf<IModelMetadataFilter>();
			});
		}
	}
}