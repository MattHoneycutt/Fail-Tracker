using System.Collections.Generic;
using System.Web.Mvc;
using FailTracker.Web.Controllers;
using Moq;
using NUnit.Framework;
using SpecsFor;
using Should;
using MvcContrib.TestHelper;

namespace FailTracker.UnitTests.Web.Controllers
{
	public class FailTrackerControllerSpecs
	{
		public class when_executing_action_for_valid_model : given.request_context_for_action_with_form_param
		{
			protected override void When()
			{
				SUT.OnActionExecuting(_context.Object);
			}

			[Test]
			public void then_it_does_not_set_the_return()
			{
				_context.Object.Result.ShouldBeNull();
			}
		}

		public class when_executing_action_for_invalid_model : given.invalid_request_context_for_action_with_form_param
		{
			protected override void When()
			{
				SUT.OnActionExecuting(_context.Object);
			}

			[Test]
			public void then_it_returns_the_view_with_the_expected_model()
			{
				_context.Object.Result.AssertViewRendered().WithViewData<TestForm>().ShouldNotBeNull();
			}
		}

		public static class given
		{
			public abstract class request_context_for_action_with_form_param : SpecsFor<TestFailTrackerController>
			{
				protected Mock<ActionExecutingContext> _context;

				protected override void Given()
				{
					var parameter = GetMockFor<ParameterDescriptor>();
					parameter.SetupGet(p => p.ParameterType.Name).Returns("MyForm");

					var controller = GetMockFor<ControllerBase>();
					controller.Object.ViewData = new ViewDataDictionary();

					_context = GetMockFor<ActionExecutingContext>();
					_context.Setup(c => c.ActionParameters).Returns(new Dictionary<string, object> { { "form", new TestForm() } });
					_context.Setup(c => c.ActionDescriptor.GetParameters()).Returns(new[] { parameter.Object });
					_context.Setup(c => c.Controller).Returns(controller.Object);
				}
			}

			public abstract class invalid_request_context_for_action_with_form_param : request_context_for_action_with_form_param
			{
				protected override void Given()
				{
					base.Given();

					_context.Object.Controller.ViewData.ModelState.AddModelError("test", "BLAH");
				}
			}
		}

		#region Test Classes

		public class TestFailTrackerController : FailTrackerController
		{
			public void OnActionExecuting(ActionExecutingContext context)
			{
				base.OnActionExecuting(context);
			}
		}

		public class TestForm
		{
			 
		}

		#endregion
	}

}