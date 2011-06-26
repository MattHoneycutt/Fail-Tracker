using StructureMap.Configuration.DSL;

namespace FailTracker.Web.Infrastructure.ValueProviders
{
	public class ValueProviderRegistry : Registry
	{
		public ValueProviderRegistry()
		{
			Scan(scanner =>
			     	{
			     		scanner.TheCallingAssembly();
			     		scanner.Convention<ValueProviderScanner>();
			     	});
		}
	}
}