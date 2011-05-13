using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}