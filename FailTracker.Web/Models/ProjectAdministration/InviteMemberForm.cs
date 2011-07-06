using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Models.ProjectAdministration
{
	public class InviteMemberForm
	{
		[HiddenInput]
		public Guid ProjectID { get; set; }

		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; } 
	}
}