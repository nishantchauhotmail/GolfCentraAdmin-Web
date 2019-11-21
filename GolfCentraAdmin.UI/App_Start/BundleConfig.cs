using System.Web;
using System.Web.Optimization;

namespace GolfCentraAdmin.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                       //  "~/Theme/modules/JS/jquery.min.js",
                    "~/Scripts/jquery-3.3.1.min.js",
                       "~/Scripts/jquery-ui-1.12.1.js",
                         //"~/Scripts/Custom/jquery-2.2.3.js",H:\Main Project\MergeProject\MergeProject\GCMergeProjectAdmin\GolfCentraAdmin.UI\Scripts\jquery-ui-1.12.1.js
                         // "~/Scripts/Custom/jquery.unobtrusive-ajax.js",
                         "~/Theme/modules/JS/bootstrap.min.js",
                          "~/Theme/modules/JS/c3.min.js",
                           "~/Theme/modules/JS/d3.min.js",
                            "~/Theme/modules/JS/jquery.dataTables.min.js",
                             "~/Theme/modules/JS/morris.min.js",
                              "~/Theme/modules/JS/perfect-scrollbar.jquery.min.js",
                               "~/Theme/modules/JS/popper.min.js",
                                "~/Theme/modules/JS/raphael-min.js",
                                "~/Theme/js/waves.js",
                                "~/Theme/js/sidebarmenu.js",
                                "~/Theme/js/custom.js",
                            "~/Scripts/jquery.unobtrusive-ajax.min.js",
                                "~/Scripts/Custom/Common.js",
                                "~/Theme/modules/JS/bootstrap-tagsinput.js",
                                "~/Theme/modules/JS/bootstrap-datepicker.min.js",

                                "~/Theme/modules/JS/daterangepicker.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //  bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //             "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Theme/modules/CSS/bootstrap.min.css",
                      "~/Theme/modules/CSS/c3.min.css",
                     "~/Theme/modules/CSS/morris.css",
                     "~/Theme/modules/CSS/perfect-scrollbar.css",
                     "~/Theme/css/style.css",
                     "~/Theme/css/colors/default.css",
                     "~/Theme/modules/CSS/bootstrap-tagsinput.css",
                      "~/Theme/modules/CSS/bootstrap-datepicker.min.css",

                        "~/Theme/modules/CSS/daterangepicker.css"
                     ));
        }
    }
}
