using System.Web.Mvc;
using FailTracker.Web.Infrastructure.ModelMetadata;
using FailTracker.Web.Infrastructure.ModelMetadata.Filters;
using StructureMap.Configuration.DSL;

namespace FailTracker.Web
{
	public class ModelMetadataRegistry : Registry
	{
		public ModelMetadataRegistry()
		{
			For<ModelMetadataProvider>().Use<StructureMapModelMetadataProvider>();

			Scan(scan =>
			     	{
			     		scan.TheCallingAssembly();
			     		scan.AddAllTypesOf<IModelMetadataFilter>();
			     	});
		}
	}
}