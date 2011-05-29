using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace FailTracker.Web.Infrastructure.Mapping
{
	public static class MappingBootstrapper
	{
		public static void LoadAllMaps()
		{
			var maps = (from t in Assembly.GetExecutingAssembly().GetExportedTypes()
			            from i in t.GetInterfaces()
			            where (i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IMappable<>)) ||
			                  typeof (IMappable).IsAssignableFrom(t)
			            where !t.IsAbstract &&
			                  !t.IsInterface
			            select Activator.CreateInstance(t)).ToArray();

			foreach (var map in maps)
			{
				if (map is IMappable)
				{
					((IMappable) map).CreateMappings(Mapper.Configuration);
				}
				else
				{
					var destination = map.GetType();
					var source = (from i in map.GetType().GetInterfaces()
					              where i.IsGenericType &&
					                    i.GetGenericTypeDefinition() == typeof (IMappable<>)
					              select i.GetGenericArguments()[0]).Single();

					Mapper.CreateMap(source, destination);
				}
			}
		}
	}
}