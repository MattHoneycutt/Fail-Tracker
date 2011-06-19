using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Models.Authentication
{
	public class LogOnForm
	{
		[Required]
		[DisplayName("Email Address")]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}