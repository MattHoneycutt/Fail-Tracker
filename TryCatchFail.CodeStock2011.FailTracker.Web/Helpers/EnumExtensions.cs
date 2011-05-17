using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TryCatchFail.CodeStock2011.FailTracker.Web.Helpers
{
	public static class EnumExtensions
	{
		public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum selectedValue) 
			where TEnum : struct 
		{
			if (!typeof(TEnum).IsEnum)
			{
				throw new ArgumentException("Type parameter must be an enumeration type (ie: typeof(MyEnum)).", "TEnum");
			}

			return from TEnum e in Enum.GetValues(typeof (TEnum))
			       let selected = e.Equals(selectedValue)
			       select new SelectListItem {Text = e.ToString(), Value = e.ToString(), Selected = selected};

		}
	}
}