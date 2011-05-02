using System;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Domain
{
	public class Issue
	{
		public virtual Guid ID { get; set; }

		public virtual string Title { get; set; }

		public virtual string AssignedTo { get; set; }

		public virtual string Body { get; set; }
	}
}