using System.Linq;
using System.Reflection;
using FailTracker.Core.Domain;
using NUnit.Framework;

namespace FailTracker.UnitTests.Core.Domain
{
	[TestFixture]
	public class DomainConventions
	{
		[Test]
		public void all_domain_entities_should_contain_only_virtual_members()
		{
			var types = from t in typeof (Project).Assembly.GetTypes()
			            where t.Namespace == typeof (Project).Namespace &&
			                  t.IsPublic &&
			                  !t.IsAbstract &&
							  t.IsClass
			            select t;

			var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;

			var nonVirtualMembers = (from t in types
			                         from m in t.GetMethods(bindingFlags)
			                         where !m.IsVirtual
			                         select t.Name + "." + m.Name).Union(
			                         	from t in types
										from p in t.GetProperties(bindingFlags)
			                         	where !p.GetGetMethod(true).IsVirtual
			                         	select t.Name + "." + p.Name);

			if (nonVirtualMembers.Any())
			{
				Assert.Fail("Non-virtual members found in domain type: \r\n" + string.Join("\r\n", nonVirtualMembers.ToArray()));
			}
		}
	}
}