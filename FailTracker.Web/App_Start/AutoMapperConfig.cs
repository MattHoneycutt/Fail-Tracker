using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FailTracker.Web.Infrastructure.Mapping;
using FailTracker.Web.Infrastructure.Tasks;

namespace FailTracker.Web
{
	public class AutoMapperConfig : IRunAtInit
	{
		public void Execute()
		{
			var types = Assembly.GetExecutingAssembly().GetExportedTypes();

			LoadStandardMappings(types);

			LoadCustomMappings(types);
		}

		private static void LoadCustomMappings(IEnumerable<Type> types)
		{
			var maps = (from t in types
						from i in t.GetInterfaces()
						where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
							  !t.IsAbstract &&
							  !t.IsInterface
						select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

			foreach (var map in maps)
			{
				map.CreateMappings(Mapper.Configuration);
			}
		}

		private static void LoadStandardMappings(IEnumerable<Type> types)
		{
			var maps = (from t in types
						from i in t.GetInterfaces()
						where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
							  !t.IsAbstract &&
							  !t.IsInterface
						select new
						{
							Source = i.GetGenericArguments()[0], 
							Destination=t
						}).ToArray();

			foreach (var map in maps)
			{
				Mapper.CreateMap(map.Source, map.Destination);
			}
		}
	}
}