using System.Web;
using System.Web.Optimization;

namespace TBSLogistics.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));



            bundles.Add(new StyleBundle("~/Admin/css").Include(
                "~/Areas/Admin/Statics/vendor/fontawesome-free/css/all.min.css",
                 "~/Areas/Admin/Statics/css/MyCss.css",
                     "~/Areas/Admin/Statics/css/toastr.min.css",
                      "~/Areas/Admin/Statics/css/sb-admin-2.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                   "~/Areas/Admin/Statics/vendor/jquery/jquery.min.js",
                    "~/Areas/Admin/Statics/vendor/bootstrap/js/bootstrap.bundle.min.js",
                     "~/Areas/Admin/Statics/vendor/jquery-easing/jquery.easing.min.js",
                       "~/Areas/Admin/Statics/js/toastr.min.js",
                        "~/Areas/Admin/Statics/js/my-js.js",
                            "~/Areas/Admin/Statics/js/sb-admin-2.min.js"
                   ));
            bundles.Add(new StyleBundle("~/User/css").Include(
              "~/Content/css/core-style.css",
                  "~/Areas/Admin/Statics/css/toastr.min.css",
                 "~/Content/css/style.css"));
            bundles.Add(new ScriptBundle("~/bundles/user").Include(
                    "~/Scripts/js/jquery/jquery-2.2.4.min.js",
                    "~/Scripts/js/popper.min.js",
                           "~/Scripts/js/myJS.js",
                    "~/Scripts/js/bootstrap.min.js",
                          "~/Scripts/js/plugins.js",
                                 "~/Scripts/js/active.js",
                                               "~/Areas/Admin/Statics/js/toastr.min.js"
                ));

        }
    }
}
