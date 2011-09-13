using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;
using FailTracker.Core.Domain;

namespace FailTracker.Core.Data
{
	public static class NHibernateBootstrapper
	{
		private static ISessionFactory _sessionFactory;
		private static Configuration _configuration;

		public static void Bootstrap()
		{
			var stdConfig = new Configuration();
			stdConfig.Configure();
			stdConfig.AddAuxiliaryDatabaseObject(new SimpleAuxiliaryDatabaseObject("alter table UserToProject add constraint PK_UserToProject primary key (User_id, Project_id)", ""));

			_configuration = Fluently.Configure(stdConfig)
				.Mappings(m => m.AutoMappings.Add(
					AutoMap.AssemblyOf<Issue>(new FailTrackerConfig()).UseOverridesFromAssemblyOf<IssueOverrides>()
					)
				)
				.BuildConfiguration();

			_sessionFactory = _configuration.BuildSessionFactory();
		}

		public static void UpdateSchema()
		{
			new SchemaUpdate(_configuration)
				.Execute(false, true);
		}

		public static void CreateSchema()
		{
			new SchemaExport(_configuration)
				.Execute(false, true, false);
		}

		public static ISession GetSession()
		{
			return _sessionFactory.OpenSession();
		}
	}
}