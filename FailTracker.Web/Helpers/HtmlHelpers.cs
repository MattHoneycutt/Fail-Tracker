using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FailTracker.Web.Models.Helpers;
using FailTracker.Web.Models.Shared;

namespace FailTracker.Web.Helpers
{
	public static class HtmlHelpers
	{
		public static MvcHtmlString ProjectToolbarFor<TModel>(this HtmlHelper<TModel> helper, Guid projectID)
		{
			var model = new ProjectToolbarViewModel(projectID);
			return helper.DisplayFor(_ => model);
		}

		public static IssueGridBuilder<TModel> DisplayIssueGrid<TModel>(this HtmlHelper<TModel> helper, IEnumerable<IssueSummaryViewModel> issues)
		{
			return new IssueGridBuilder<TModel>(helper, issues);
		}
	}
}