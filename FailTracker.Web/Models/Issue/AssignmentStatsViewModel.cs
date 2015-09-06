using System.Linq;
using AutoMapper;
using FailTracker.Web.Domain;
using Heroic.AutoMapper;

namespace FailTracker.Web.Models.Issue
{
	public class AssignmentStatsViewModel : IHaveCustomMappings
	{
		public string UserName { get; set; }
		public int Enhancements { get; set; }
		public int Bugs { get; set; }
		public int Support { get; set; }
		public int Other { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<ApplicationUser, AssignmentStatsViewModel>()
				.ForMember(m => m.Enhancements, opt => 
					opt.MapFrom(u => u.Assignments.Count(i => i.IssueType == IssueType.Enhancement)))
				.ForMember(m => m.Bugs, opt =>
					opt.MapFrom(u => u.Assignments.Count(i => i.IssueType == IssueType.Bug)))
				.ForMember(m => m.Support, opt =>
					opt.MapFrom(u => u.Assignments.Count(i => i.IssueType == IssueType.Support)))
				.ForMember(m => m.Other, opt =>
					opt.MapFrom(u => u.Assignments.Count(i => i.IssueType == IssueType.Other)));

		}
	}
}