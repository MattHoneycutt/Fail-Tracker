using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Features.Dashboard
{
	public class GetIssueHeadersQuery : QueryBase<GetIssueHeadersQuery.Options,GetIssueHeadersQuery.Result[]>
	{
		private readonly ISession _session;

		public GetIssueHeadersQuery(ISession session)
		{
			_session = session;
		}

		public class Options
		{
		}

		public class Result
		{
			public Guid ID { get; set; }

			public string Title { get; set; }

			public string AssignedTo { get; set; }
		}

		public override Result[] Run(Options options)
		{
			return (from i in _session.Query<Issue>()
			        select new Result {ID = i.ID, Title = i.Title, AssignedTo = i.AssignedTo}).ToArray();
		}

	}
}