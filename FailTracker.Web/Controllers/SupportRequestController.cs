using System.Net.Mail;
using System.Web.Mvc;
using FailTracker.Web.Models.SupportRequest;
using Microsoft.Web.Mvc;

namespace FailTracker.Web.Controllers
{
	public class SupportRequestController : FailTrackerController
	{
		public ActionResult Submit()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Submit(SubmitSupportRequestForm form)
		{
			try
			{
				using (var client = new SmtpClient())
				{
					client.Send(form.From, "mbhoneycutt@gmail.com", form.Subject, form.Body);
				}
			}
			//Who cares? They're just paying us!
			catch
			{
				
			}

			TempData["From"] = form.From;
			return this.RedirectToAction(c => c.ThankYou());
		}

		public ActionResult ThankYou()
		{
			return View(new SupportAcknowledgementViewModel{From = (string) TempData["From"]});
		}
	}
}