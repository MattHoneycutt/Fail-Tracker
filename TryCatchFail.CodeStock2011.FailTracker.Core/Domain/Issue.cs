using System;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Domain
{
	public class Issue : IEquatable<Issue>
	{
		public virtual Guid ID { get; set; }

		public virtual string Title { get; set; }

		public virtual string AssignedTo { get; set; }

		public virtual string Body { get; set; }

		public static Issue Create(string title, string assignedTo, string body)
		{
			return new Issue { Title = title, AssignedTo = assignedTo, Body = body };
		}

		//Required for NHibernate
		protected Issue()
		{
			
		}

		#region Equals Implementation 

		public virtual bool Equals(Issue other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.ID.Equals(ID) && Equals(other.Title, Title) && Equals(other.Body, Body) && Equals(other.AssignedTo, AssignedTo);
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
				result = (result*397) ^ (Body != null ? Body.GetHashCode() : 0);
				result = (result*397) ^ (AssignedTo != null ? AssignedTo.GetHashCode() : 0);
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