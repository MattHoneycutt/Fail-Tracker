using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Models.SignUp
{
	public class SignUpForm
	{
		[Required]
		[EmailAddress]
		[DisplayName("Email Address")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Password (again)")]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string PasswordAgain { get; set; }
	}
}