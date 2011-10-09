using FailTracker.Core.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace FailTracker.Core.Data
{
	public class ProjectOverrides : IAutoMappingOverride<Project>
	{
		public void Override(AutoMapping<Project> mapping)
		{
			mapping.HasManyToMany(p => p.Members);
		}
	}
}