using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;
using FailTracker.Web.Infrastructure.ModelMetadata;

namespace FailTracker.Web.Models.Issues
{
	public class EditIssueForm : IHaveCustomMappings
	{
		[HiddenInput(DisplayValue = false)]
		public Guid ID { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public IssueType Type { get; set; }

		[Required]
		public PointSize Size { get; set; }

		[DisplayName("Assigned To")]
		[UIHint("UserDropDown")]
		public Guid? AssignedToID { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Comments { get; set; }

		[Required]
		[RenderMode(RenderMode.None)]
		public User CurrentUser { get; set; }

		void IHaveCustomMappings.CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<Issue, EditIssueForm>()
				.ForMember(f => f.Comments, opt => opt.Ignore())
				.ForMember(f => f.CurrentUser, opt => opt.Ignore());
		}
	}
}