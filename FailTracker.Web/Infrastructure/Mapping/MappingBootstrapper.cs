using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FailTracker.Web.Infrastructure.Startup;

namespace FailTracker.Web.Infrastructure.Mapping
{
	public class MappingBootstrapper : IRunAtStartup
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
			            where typeof (IHaveCustomMappings).IsAssignableFrom(t) &&
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
			            where i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IMapFrom<>) &&
			                  !t.IsAbstract &&
			                  !t.IsInterface
			            select Activator.CreateInstance(t)).ToArray();

			foreach (var map in maps)
			{
				var destination = map.GetType();
				var source = (from i in map.GetType().GetInterfaces()
								where i.IsGenericType &&
									i.GetGenericTypeDefinition() == typeof(IMapFrom<>)
								select i.GetGenericArguments()[0]).Single();

				Mapper.CreateMap(source, destination);
			}
		}
	}
}