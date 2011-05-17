using System;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Domain
{
	public class Issue : IEquatable<Issue>
	{
		//TODO: We only want to expose changing the ID for test purposes...
		public virtual Guid ID { get; set; }

		public virtual string Title { get; protected set; }

		public virtual User AssignedTo { get; protected set; }

		public virtual string Body { get; protected set; }

		public virtual IssueType Type { get; protected set; }

		public virtual PointSize Size { get; protected set; }

		public static Issue CreateNewStory(string title, User assignedTo, string body)
		{
			return new Issue { Title = title, AssignedTo = assignedTo, Body = body, Type = IssueType.Story };
		}
		
		public static Issue CreateNewBug(string title, User assignedTo, string body)
		{
			return new Issue { Title = title, AssignedTo = assignedTo, Body = body, Type = IssueType.Bug };
		}
		
		//Required for NHibernate
		protected Issue()
		{
			
		}

		public virtual Issue ChangeTypeTo(IssueType type)
		{
			Type = type;

			return this;
		}

		public virtual Issue SetSizeTo(PointSize size)
		{
			Size = size;

			return this;
		}

		#region Equals Implementation 

		public virtual bool Equals(Issue other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.ID.Equals(ID) && Equals(other.Title, Title) && Equals(other.AssignedTo, AssignedTo) && Equals(other.Body, Body) && Equals(other.Type, Type) && Equals(other.Size, Size);
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
				result = (result*397) ^ (Body != null ? Body.GetHashCode() : 0);
				result = (result*397) ^ Type.GetHashCode();
				result = (result*397) ^ Size.GetHashCode();
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