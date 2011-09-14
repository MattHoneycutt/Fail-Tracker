using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FailTracker.Core.Domain;
using FailTracker.Web.Infrastructure.Mapping;
using FailTracker.Web.Infrastructure.ModelMetadata.Attributes;

namespace FailTracker.Web.Models.Profile
{
	public class ProfileViewModel : IHaveCustomMappings
	{
		[RenderMode(RenderMode.Display)]
		public string EmailAddress { get; set; }

		[Required]
		[Compare("ConfirmPassword", ErrorMessage = "Passwords must match!")]
		[DataType(DataType.Password)]
		[StringLength(20, MinimumLength = 7)]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<User, ProfileViewModel>()
				.ForMember(p => p.NewPassword, opt => opt.Ignore())
				.ForMember(p => p.ConfirmPassword, opt => opt.Ignore());
		}
	}
}