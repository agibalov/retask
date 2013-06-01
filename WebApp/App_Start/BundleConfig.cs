using System.Collections.Generic;
using System.IO;
using System.Web.Optimization;

namespace WebApp
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/pagedown/Markdown.Converter.js",
                "~/Scripts/jquery-{version}.min.js",
                "~/Scripts/jquery-ui-{version}.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-cookies.min.js",
                "~/Scripts/underscore.min.js",
                "~/Scripts/App/App.js", 
                "~/Scripts/App/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/retask.css"));
        }
    }
}