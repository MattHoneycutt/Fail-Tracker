using System.Linq;
using FailTracker.Web.Data;
using FailTracker.Web.Domain;
using FailTracker.Web.Infrastructure.Tasks;

namespace FailTracker.Web
{
	public class SeedData : IRunAtStartup
	{
		private readonly ApplicationDbContext _context;

		public SeedData(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Execute()
		{
		    foreach (var user in _context.Users.Where(u => u.UserName == "Matt").OrderBy(u => u.Id).Skip(1))
		    {
		        _context.Users.Remove(user);
		    }

		    _context.SaveChanges();

            var user1 = _context.Users.FirstOrDefault() ??
			            _context.Users.Add(new ApplicationUser {UserName = "JuneTodd"});

			var user2 = _context.Users.FirstOrDefault(u => u.UserName == "DougStone") ??
			            _context.Users.Add(new ApplicationUser { UserName = "DougStone" });

			var user3 = _context.Users.FirstOrDefault(u => u.UserName == "GarrettHoward") ??
						_context.Users.Add(new ApplicationUser { UserName = "GarrettHoward" });

			_context.SaveChanges();

			if (!_context.Issues.Any())
			{
				_context.Issues.Add(new Issue(user2, user1, IssueType.Bug, "Viewing details crashes", "Sometimes, viewing an issue's details will cause a crash.  It seems to only happen when there is a full moon out!"));
				_context.Issues.Add(new Issue(user3, user1, IssueType.Support, "Second account", "I need a second account for my cat to use.  My cat finds all kinds of bugs, and I really want him to be able to log the issues himself."));
				_context.Issues.Add(new Issue(user1, user2, IssueType.Enhancement, "Tablet-Friendly UX", "I'd like to see the app support use from a tablet.  The web app works from a tablet, but it's clunky.  I want the UX to be streamlined and optimized for touch."));

				_context.SaveChanges();
			}
		}
	}
}