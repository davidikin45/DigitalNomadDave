using System.Web;
using System.Web.Optimization;

namespace DND
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/js/all").Include(
                      "~/bower_components/jQuery/dist/jquery.js",
                       //"~/bower_components/jquery-ui/jquery-ui.js",
                      "~/bower_components/jquery-validate-unobtrusive/jquery.validate.unobtrusive.js",
                      "~/bower_components/tether/dist/js/tether.js",
                      "~/bower_components/bootstrap/dist/js/bootstrap.js",
                      "~/js/hash.js",
                      "~/bower_components/angular/angular.js",
                      "~/bower_components/angular-cookies/angular-cookies.js",
                      "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.js",
                      "~/bower_components/angular-animate/angular-animate.js",
                      "~/bower_components/angular-ui-grid/ui-grid.js",
                      "~/bower_components/respond/dest/respond.min.js",
                       "~/bower_components/angularjs-slider/dist/rzslider.js",
                       "~/bower_components/underscore/underscore-min.js",
                       "~/bower_components/instafeed.js/instafeed.js",
                       "~/bower_components/magnific-popup/dist/jquery.magnific-popup.js",
                       "~/bower_components/ngmap/build/scripts/ng-map.js",
                        "~/js/db-geography.js")
                      .IncludeDirectory("~/js/app/", "*.js", true)
                      .Include("~/js/multiselect-tpls.js")
                      .Include("~/js/app/app.js")
                      .Include("~/js/modal.js")
                      .Include("~/js/return-to-top.js")
                      .Include("~/js/scroll-position.js")
                       .Include("~/js/site.js")
                       );

            bundles.Add(new ScriptBundle("~/js/admin/all").Include(
                      "~/bower_components/tinymce/tinymce.js")
                       .Include("~/js/admin.js"));

           //respond = media queries < ie8
           bundles.Add(new StyleBundle("~/css/all").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.css",
                      "~/bower_components/font-awesome/css/font-awesome.css",
                      "~/bower_components/angular-ui-grid/ui-grid.css",
                       "~/bower_components/angularjs-slider/dist/rzslider.css",
                       "~/css/multiselect.css",
                       "~/css/menu.css",
                       "~/css/flags16.css",
                        "~/css/flags32.css",
                       "~/css/modal.css",
                        "~/css/polaroid-images.css",
                       "~/css/site-xs.css",
                       "~/css/site-sm.css",
                       "~/css/site-md.css",
                       "~/css/site-lg.css",
                       "~/css/site-xl.css",
                        "~/css/return-to-top.css",
                       "~/bower_components/magnific-popup/dist/magnific-popup.css",
                       "~/css/blog.css")
                      );

            bundles.Add(new StyleBundle("~/css/admin/all").Include(
                "~/css/admin.css"));

           System.Web.Optimization.BundleTable.EnableOptimizations = true;
        }
    }
}
