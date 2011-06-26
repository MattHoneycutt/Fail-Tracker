using System.Web.Mvc;

namespace FailTracker.Web.Infrastructure.ModelBinding
{
	//TODO: Remove?
	public class SuperModelBinder : IModelBinder
	{
		private readonly IModelBinder _standardBinder;

		public SuperModelBinder(IModelBinder standardBinder)
		{
			_standardBinder = standardBinder;
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return _standardBinder.BindModel(controllerContext, bindingContext);
		}
	}
}