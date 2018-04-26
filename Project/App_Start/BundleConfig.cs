using System.Web;
using System.Web.Optimization;

namespace Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/umd/popper.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/UserProfile_Index").Include(
                "~/Scripts/UserProfile.Index.js"));


            bundles.Add(new StyleBundle("~/Content/DateTimePicker").Include(
                "~/Content/tempusdominus-bootstrap-4.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/DateTimePicker").Include(
                "~/Scripts/moment-with-locales.min.js",
                "~/Scripts/tempusdominus-bootstrap-4.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Admin").Include(
                "~/Scripts/Admin.js"));

        }
    }
}
