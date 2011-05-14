using System;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Domain
{
	public class User
	{
		public virtual Guid ID { get; set; }

		public virtual string EmailAddress { get; set; }

		public virtual string Password { get; set; }
	}
}