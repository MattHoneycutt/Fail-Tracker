using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;

namespace FailTracker.Web.Models.Issues
{
	public class ChangeViewModel : IMappable
	{
		[DataType("User")]
		public string EditedBy { get; set; }

		public DateTime ChangedAt { get; set; }

		public string Comments { get; set; }
		
		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Change, ChangeViewModel>();
		}
	}
}