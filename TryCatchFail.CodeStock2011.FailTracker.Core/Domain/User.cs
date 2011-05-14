using System;
using System.Security.Cryptography;
using System.Text;

namespace TryCatchFail.CodeStock2011.FailTracker.Core.Domain
{
	public class User
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
	}
}