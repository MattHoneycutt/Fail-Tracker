using System;
using System.Collections.Generic;

namespace FailTracker.Core.Domain
{
	public class Issue : IEquatable<Issue>
	{
		private Change _activeChange;

		//TODO: We only want to expose changing the ID for test purposes...
		public virtual Guid ID { get; set; }

		public virtual string Title { get; protected set; }

		public virtual User AssignedTo { get; protected set; }

		public virtual string Description { get; protected set; }

		public virtual IssueType Type { get; protected set; }

		public virtual PointSize Size { get; protected set; }

		public virtual bool IsUnassigned
		{
			get { return AssignedTo == null; }
		}

		public virtual User CreatedBy { get; protected set; }

		public virtual DateTime CreatedAt { get; protected set; }

		public virtual IEnumerable<Change> Changes { get; protected set; }

		public virtual Project Project { get; protected set; }

		public static Issue CreateNewIssue(Project project, string title, User creator, string body)
		{
			var issue = new Issue
			       	{
			       		Title = title,
			       		CreatedBy = creator,
			       		Description = body,
			       		Type = IssueType.Story,
			       		CreatedAt = DateTime.Now
			       	};

			//This allows a newly-created story to be edited.
			issue._activeChange = Change.Empty;

			project.AttachIssue(issue);

			return issue;
		}
		
		//Required for NHibernate
		protected Issue()
		{
			Changes = new List<Change>();
		}

		private void EnsureEditModeEnabled()
		{
			if (_activeChange == null)
			{
				throw new InvalidOperationException("Issue must be in edit mode before you can make changes.  Call the BeginEdit method first.");
			}
		}

		public virtual Issue ChangeTypeTo(IssueType type)
		{
			EnsureEditModeEnabled();

			_activeChange.IsTypeChanged = Type != type;

			Type = type;

			return this;
		}

		public virtual Issue ChangeSizeTo(PointSize size)
		{
			EnsureEditModeEnabled();

			_activeChange.IsPointSizeChanged = Size != size;

			Size = size;

			return this;
		}

		public virtual Issue ReassignTo(User user)
		{
			EnsureEditModeEnabled();

			_activeChange.IsReassigned = AssignedTo != user;

			AssignedTo = user;

			return this;
		}

		public virtual Issue ChangeTitleTo(string title)
		{
			EnsureEditModeEnabled();

			_activeChange.IsTitleChanged = Title != title;

			Title = title;

			return this;
		}

		public virtual Issue ChangeDescriptionTo(string description)
		{
			EnsureEditModeEnabled();

			_activeChange.IsDescriptionChanged = Description != description;

			Description = description;

			return this;
		}

		public virtual void BeginEdit(User editingUser, string comments)
		{
			_activeChange = Change.For(this, editingUser, comments);

			((IList<Change>)Changes).Add(_activeChange);
		}

		public virtual void EndEdit()
		{
			_activeChange = null;
		}

		#region Equals Implementation 

		public virtual bool Equals(Issue other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.ID.Equals(ID) && Equals(other.Title, Title) && Equals(other.AssignedTo, AssignedTo) && Equals(other.Description, Description) && Equals(other.Type, Type) && Equals(other.Size, Size) && Equals(other.CreatedBy, CreatedBy);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Issue)) return false;
			return Equals((Issue) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = ID.GetHashCode();
				result = (result*397) ^ (Title != null ? Title.GetHashCode() : 0);
				result = (result*397) ^ (AssignedTo != null ? AssignedTo.GetHashCode() : 0);
				result = (result*397) ^ (Description != null ? Description.GetHashCode() : 0);
				result = (result*397) ^ Type.GetHashCode();
				result = (result*397) ^ Size.GetHashCode();
				result = (result*397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
				return result;
			}
		}

		public static bool operator ==(Issue left, Issue right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Issue left, Issue right)
		{
			return !Equals(left, right);
		}

		#endregion
	}
}