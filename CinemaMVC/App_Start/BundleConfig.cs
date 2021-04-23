using System.Web;
using System.Web.Optimization;

namespace CinemaMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusiveajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
            "~/Scripts/sweetalert2.min.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/bundles/bootstrap-chosen-Styles").Include(
                      "~/Content/bootstrap-chosen.css"));
            // chosen 
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-chosen").Include(
                      "~/Scripts/chosen.jquery.min.js"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //  "~/Content/themes/base/jquery.ui.core.css",
            //  "~/Content/themes/base/jquery.ui.resizable.css",
            //  "~/Content/themes/base/jquery.ui.selectable.css",
            //  "~/Content/themes/base/jquery.ui.accordion.css",
            //  "~/Content/themes/base/jquery.ui.autocomplete.css",
            //  "~/Content/themes/base/jquery.ui.button.css",
            //  "~/Content/themes/base/jquery.ui.dialog.css",
            //  "~/Content/themes/base/jquery.ui.slider.css",
            //  "~/Content/themes/base/jquery.ui.tabs.css",
            //  "~/Content/themes/base/jquery.ui.datepicker.css",
            //  "~/Content/themes/base/jquery.ui.progressbar.css",
            //  "~/Content/themes/base/jquery.ui.theme.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                      "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(                     
                      "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sweetalert2.min.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(            
              "~/Content\fontawesome\font-awesome.min.css"));
        }
    }
}
