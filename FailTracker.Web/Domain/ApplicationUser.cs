using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FailTracker.Web.Domain
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
	    public IList<Issue> Assignments { get; set; }
    }
}