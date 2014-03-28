using FailTracker.Web.Domain;

namespace FailTracker.Web.Infrastructure
{
	public interface ICurrentUser
	{
		ApplicationUser User { get; } 
	}
}