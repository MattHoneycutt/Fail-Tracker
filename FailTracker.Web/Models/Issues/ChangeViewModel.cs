using System;
using System.ComponentModel.DataAnnotations;

namespace FailTracker.Web.Models.Issues
{
	public class ChangeViewModel
	{
		[DataType("User")]
		public string EditedBy { get; set; }

		public DateTime ChangedAt { get; set; }

		public string Comments { get; set; }
	}
}