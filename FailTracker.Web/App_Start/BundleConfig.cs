using System.Web.Optimization;

namespace FailTracker.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libraries").Include(
                        "~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery.validate*",
						"~/Scripts/underscore.js"
						));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
					  "~/Scripts/rowlink.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                    .Include("~/Scripts/angular.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/app")
                    .Include("~/Scripts/app/failtrackerApp.js")
					.IncludeDirectory("~/Scripts/app/utilities", "*.js")
					.IncludeDirectory("~/Scripts/app/controllers", "*.js")
					.IncludeDirectory("~/Scripts/app/services", "*.js")
                );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/ubuntu.theme.css",
                      "~/Content/site.css"));
        }
    }
}
