using AutoMapper;

namespace FailTracker.Web.Infrastructure.Mapping
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(IConfiguration configuration);
	}
}