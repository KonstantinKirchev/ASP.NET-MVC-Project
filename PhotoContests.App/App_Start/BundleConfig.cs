using System.Web;
using System.Web.Optimization;

namespace PhotoContests.App
{
    public class BundleConfig
    {
<<<<<<< HEAD
=======
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/lightbox.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/jquery.signalR-*"));

<<<<<<< HEAD
=======
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/lightbox.css"));
<<<<<<< HEAD

            bundles.Add(new StyleBundle("~/adminKendo/css").Include(
                      "~/Content/kendo/kendo.common.core.min.css",
                      "~/Content/kendo/kendo.silver.min.css",
                      "~/Content/kendo/kendo.common.min.css"));

            bundles.Add(new ScriptBundle("~/adminKendo/bootstrap").Include(
                      "~/Scripts/kendo/kendo.all.min.js",
                      "~/Scripts/kendo/kendo.aspnetmvc.min.js"));
=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
        }
    }
}
