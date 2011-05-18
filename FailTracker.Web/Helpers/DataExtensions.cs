using System.Web.Mvc;
using StructureMap;
using FailTracker.Core.Data;
using FailTracker.Core.Domain;

namespace FailTracker.Web.Helpers
{
	//NOTE: I'm not really all that happy with this class, but it seems
	//		to be the simplest solution for now...
	public static class DataExtensions
	{
		public static SelectList GetUserSelectList(this HtmlHelper helper)
		{
			var repository = ObjectFactory.GetInstance<IRepository<User>>();

			return new SelectList(repository.Query(), "ID", "EmailAddress");
		}
	}
}