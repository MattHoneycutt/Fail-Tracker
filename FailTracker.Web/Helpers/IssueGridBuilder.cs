using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FailTracker.Web.Models.Helpers;
using FailTracker.Web.Models.Shared;

namespace FailTracker.Web.Helpers
{
	public class IssueGridBuilder<TModel> : IHtmlString
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly IEnumerable<IssueSummaryViewModel> _issues;
		private Guid _projectID;
		private bool _allowReorder;

		public IssueGridBuilder(HtmlHelper<TModel> helper, IEnumerable<IssueSummaryViewModel> issues)
		{
			_helper = helper;
			_issues = issues;
		}

		public string ToHtmlString()
		{
			var model = new IssueGridViewModel(_projectID, _issues) {AllowReordering = _allowReorder};

			var builder = new StringBuilder(_helper.DisplayFor(_ => model).ToHtmlString());

			if (_projectID != Guid.Empty)
			{
				builder.Append(_helper.ProjectToolbarFor(_projectID));
			}

			return builder.ToString();
		}

		public override string ToString()
		{
			return ToHtmlString();
		}

		public IssueGridBuilder<TModel> WithProjectToolBar(Guid projectID)
		{
			_projectID = projectID;
			return this;
		}

		public IssueGridBuilder<TModel> AllowReordering()
		{
			_allowReorder = true;
			return this;
		}
	}
}