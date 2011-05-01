using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Data
{
	public class FailTrackerConfig : DefaultAutomappingConfiguration
	{
		public override bool IsId(Member member)
		{
			return member.Name == "ID";
		}

		public override bool ShouldMap(Type type)
		{
			if (type.Namespace != typeof(Issue).Namespace)
			{
				return false;
			}

			return base.ShouldMap(type);
		}
	}
}