using System.ComponentModel.DataAnnotations;

namespace FailTracker.Web.Models.ProjectAdministration
{
	public class InviteMemberForm
	{
		[Required]
		public string EmailAddress { get; set; } 
	}
}