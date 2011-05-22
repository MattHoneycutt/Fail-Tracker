using System;
using System.Collections.Generic;
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
		public static IEnumerable<SelectListItem> GetUserSelectList(this HtmlHelper helper, Guid? value)
		{
			var repository = ObjectFactory.GetInstance<IRepository<User>>();

			return new SelectList(repository.Query(), "ID", "EmailAddress", value);
		}
	}
}