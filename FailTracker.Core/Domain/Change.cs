using System;

namespace FailTracker.Core.Domain
{
	public class Change
	{
		public static Change Empty = new Change();

		public virtual Guid ID { get; protected set; }

		public virtual User EditedBy { get; protected internal set; }

		public virtual DateTime ChangedAt { get; protected set; }

		public virtual string Comments { get; protected internal set; }

		public virtual Issue AppliesTo { get; protected internal set; }

		public virtual bool IsReassigned { get; protected internal set; }

		public virtual bool IsTitleChanged { get; protected internal set; }

		public virtual bool IsPointSizeChanged { get; protected internal set; }

		public virtual bool IsTypeChanged { get; protected internal set; }

		public virtual bool IsDescriptionChanged { get; protected internal set; }

		public static Change For(Issue issue, User editingUser, string comment)
		{
			return new Change
			       	{
			       		EditedBy = editingUser,
			       		AppliesTo = issue,
			       		Comments = comment
			       	};
		}

		protected Change()
		{
			ChangedAt = DateTime.Now;
		}
	}
}