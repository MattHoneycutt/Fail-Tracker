using AutoMapper;

namespace FailTracker.Web.Infrastructure.Mapping
{
	public interface IMappable<T>
	{
		
	}

	public interface IMappable
	{
		void CreateMappings(IConfiguration configuration);
	}
}