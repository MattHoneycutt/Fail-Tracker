using System;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Models.Backlog
{
	public class ReorderForm
	{
		public Guid ProjectID { get; set; }
		public Guid MovedID { get; set; }
		public Guid RelativeToID { get; set; }
		public MoveType MoveType { get; set; }
	}
}