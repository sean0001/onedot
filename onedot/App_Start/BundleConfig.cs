using System.Web;
using System.Web.Optimization;

namespace one.OneDot
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/bootstrap.js"
                       

                       ));


            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));


            ///前端Css 加载
            bundles.Add(new StyleBundle("~/content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-theme.css",
                        "~/Content/toastr.css",
                        "~/Content/WebSite.css"));




            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));


            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/content/bootstrap/css/css").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/content/bootstrap/css/bootstrap-theme.css"
                ));



            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));



            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                "~/Content/kendo/2013020918/kendo.common.css",
                "~/Content/site.css"
                ));



            bundles.Add(new ScriptBundle("~/Scripts/jq").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js"
                  ));



            bundles.Add(new ScriptBundle("~/Scripts/extend").Include(
                "~/Scripts/kendo/2013020918/kendo.all.js",
                "~/Scripts/kendo/2013020918/kendo.aspnetmvc.js"
                  ));


        }
    }
}
