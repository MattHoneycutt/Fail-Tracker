using System.Web.Mvc;
using StructureMap;
using TryCatchFail.CodeStock2011.FailTracker.Core.Data;
using TryCatchFail.CodeStock2011.FailTracker.Core.Domain;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Helpers
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