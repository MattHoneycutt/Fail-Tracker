using System;
using System.Security.Cryptography;
using System.Text;

namespace FailTracker.Core.Domain
{
	public class User : IEquatable<User>
	{
		public virtual Guid ID { get; set; }

		public virtual string EmailAddress { get; set; }

		public virtual string PasswordHash { get; set; }

		public virtual string PasswordSalt { get; protected set; }

		protected User()
		{

		}
		
		public static User CreateNewUser(string emailAddress, string password)
		{
			var user = new User {EmailAddress = emailAddress};
			user.SetPassword(password);

			return user;
		}

		public virtual void SetPassword(string password)
		{
			GenerateNewSalt();

			PasswordHash = HashPassword(password);
		}

		public virtual bool IsThisTheUsersPassword(string password)
		{
			var hash = HashPassword(password);

			return hash == PasswordHash;
		}

		private string HashPassword(string password)
		{
			var hasher = MD5.Create();
			return Encoding.UTF8.GetString(hasher.ComputeHash(Encoding.UTF8.GetBytes(PasswordSalt + password)));
		}

		private void GenerateNewSalt()
		{
			RNGCryptoServiceProvider cryptoService = new RNGCryptoServiceProvider();
			byte[] buffer = new byte[10];
			cryptoService.GetBytes(buffer);
			PasswordSalt = Encoding.UTF8.GetString(buffer);
		}

		#region Equality

		public virtual bool Equals(User other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.EmailAddress, EmailAddress);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (User)) return false;
			return Equals((User) obj);
		}

		public override int GetHashCode()
		{
			return (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
		}

		public static bool operator ==(User left, User right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(User left, User right)
		{
			return !Equals(left, right);
		}

		#endregion
	}
}