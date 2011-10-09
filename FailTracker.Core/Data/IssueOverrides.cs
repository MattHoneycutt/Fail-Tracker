using FailTracker.Core.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace FailTracker.Core.Data
{
	public class IssueOverrides : IAutoMappingOverride<Issue>
	{
		public void Override(AutoMapping<Issue> mapping)
		{
			mapping.HasMany(i => i.Changes)
				.Cascade.All();
		}
	}
}