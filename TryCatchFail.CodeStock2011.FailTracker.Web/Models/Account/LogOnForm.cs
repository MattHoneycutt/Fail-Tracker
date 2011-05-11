using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Models.Account
{
	public class LogOnForm
	{
		[Required]
		[DisplayName("Email Address")]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		[Compare("PasswordAgain", ErrorMessage = "Passwords must be equal.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DisplayName("Password (again)")]
		[DataType(DataType.Password)]
		public string PasswordAgain { get; set; }
	}
}