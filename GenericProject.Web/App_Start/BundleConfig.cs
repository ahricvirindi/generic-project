using System.Web.Optimization;

namespace GenericProject.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
           #region Script bundles

            bundles.Add(new ScriptBundle("~/Scripts/app-lib-js").Include(
                "~/Scripts/lib/jquery/jquery.js",
                "~/Scripts/lib/bootstrap.js",
                "~/Scripts/lib/holder.js",
                "~/Scripts/lib/pines-notify/jquery.pnotify.js",
                "~/Scripts/lib/angular-pines-notify/src/pnotify.js",
                "~/Scripts/lib/spinjs/spin.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/angular-js").Include(
                "~/Scripts/lib/angular/angular.js",
                "~/Scripts/lib/angular-resource/angular-resource.js",
                "~/Scripts/lib/angular-cookies/angular-cookies.js",
                "~/Scripts/lib/angular-sanitize/angular-sanitize.js",
                "~/Scripts/lib/angular-route/angular-route.js",
                "~/Scripts/lib/angular-ui-utils/ui-utils.js",
                "~/Scripts/lib/angular-bootstrap/ui-bootstrap.js",
                "~/Scripts/lib/angular-bootstrap/ui-bootstrap-tpls.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/app-start-js").Include(
                "~/Scripts/app.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/app-controllers-js").Include(
                "~/Scripts/controllers/MainController.js",
                "~/Scripts/controllers/PeepsController.js",
                "~/Scripts/controllers/StyleController.js"
                ));


            bundles.Add(new ScriptBundle("~/Scripts/app-services-js").Include(
                "~/Scripts/services/ApiService.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/app-directives-js").Include(
                "~/Scripts/directives/SpinDirective.js",
                "~/Scripts/directives/OrderByDirective.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/app-models-js").Include(
                "~/Scripts/modesl/Base.js"
                ));
            #endregion

            #region Style bundles

            bundles.Add(new StyleBundle("~/Content/main/css").Include(
                "~/Content/less/main.css"
                ));

            bundles.Add(new StyleBundle("~/Content/app-lib-css").Include(
                "~/Scripts/lib/pines-notify/jquery.pnotify.default.css",
                "~/Scripts/lib/pines-notify/jquery.pnotify.default.icons.css"
                ));
        }

        #endregion
    }
}