using FailTracker.Core.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace FailTracker.Core.Data
{
	public class UserOverrides : IAutoMappingOverride<User>
	{
		public void Override(AutoMapping<User> mapping)
		{
			mapping.Map(u => u.EmailAddress).Unique();
		}
	}
}