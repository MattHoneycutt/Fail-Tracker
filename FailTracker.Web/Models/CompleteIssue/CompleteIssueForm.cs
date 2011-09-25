using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;
using FailTracker.Web.Infrastructure.ModelMetadata.Attributes;

namespace FailTracker.Web.Models.CompleteIssue
{
	public class CompleteIssueForm : IHaveCustomMappings
	{
		[HiddenInput(DisplayValue = false)]
		public Guid ID { get; set; }

		[RenderMode(RenderMode.Display)]
		public string Title { get; set; }

		[RenderMode(RenderMode.Display)]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Comments { get; set; }

		[RenderMode(RenderMode.None)]
		public User CurrentUser { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Issue, CompleteIssueForm>()
				.ForMember(f => f.CurrentUser, opt => opt.Ignore())
				.ForMember(f => f.Comments, opt => opt.Ignore());
		}
	}
}