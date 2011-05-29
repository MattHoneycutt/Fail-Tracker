using AutoMapper;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Infrastructure.Mapping
{
	public class UserToStringTypeConverter : ITypeConverter<User, string>, IHaveCustomMappings
	{
		public string Convert(ResolutionContext context)
		{
			if (context.SourceValue == null)
			{
				return null;
			}

			var user = (User) context.SourceValue;

			return user.EmailAddress;
		}

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<User, string>().ConvertUsing<UserToStringTypeConverter>();
		}
	}
}