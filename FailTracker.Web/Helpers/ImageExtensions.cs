using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using HtmlTags;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Helpers
{
	public static class ImageExtensions
	{
		public static HtmlTag ImageLink<TController>(this HtmlHelper helper, Expression<Action<TController>> action, string imageRelativeUrl) where TController : Controller
		{
			var imageUrl = UrlHelper.GenerateContentUrl(imageRelativeUrl, helper.ViewContext.HttpContext);

			var targetUrl = helper.BuildUrlFromExpression(action);

			var link = new HtmlTag("a").Attr("href", targetUrl);

			var image = new HtmlTag("img", link).Attr("src", imageUrl);

			return link;
		}
	}
}