using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using HtmlTags;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Helpers
{
	//Used as a way to organize the HtmlTag extension methods, keeps from
	//polluting the standard HtmlHelper extensions. 
	public sealed class HtmlTagHelper
	{
		public HtmlHelper HtmlHelper { get; private set; }

		public HtmlTagHelper(HtmlHelper helper)
		{
			HtmlHelper = helper;
		}
	}

	//TODO: Consider reorgnizing into its own namespace/package for reuse.
	public static class HtmlTagExtensions
	{
		public static HtmlTagHelper Tag(this HtmlHelper helper)
		{
			return new HtmlTagHelper(helper);
		}

		public static LinkTag LinkTo<TController>(this HtmlTagHelper helper, Expression<Action<TController>> action, string linkText) where TController : Controller
		{
			var target = helper.HtmlHelper.BuildUrlFromExpression(action);

			return new LinkTag(linkText, target);
		}

		public static LinkTag AsButton(this LinkTag tag, Icon icon = null)
		{
			tag.AddClass("button");

			if (icon != null)
			{
				tag.Attr("data-icon", icon);
			}

			return tag;
		}

		public static HtmlTag ButtonTo<TController>(this HtmlTagHelper helper, Expression<Action<TController>> action, Icon icon) where TController : Controller
		{
			var target = helper.HtmlHelper.BuildUrlFromExpression(action);

			var linkTag = new HtmlTag("a").Attr("href", target);
			linkTag
				.AddClass("button")
				.AddClass("ui-icon")
				.AddClass(icon.ToString());

			return linkTag;
		}
	}

	public class Icon
	{
		public static readonly Icon FolderOpen = new Icon("folder-open");
		public static readonly Icon Check = new Icon("check");
		public static readonly Icon CirclePlus = new Icon("circle-plus");
		public static readonly Icon Signal = new Icon("signal");
		public static readonly Icon Search = new Icon("search");

		private readonly string _iconName;
	
		private Icon(string iconName)
		{
			_iconName = iconName;
		}

		public override string ToString()
		{
			return "ui-icon-" + _iconName;
		}
	}
}