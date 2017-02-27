using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace CharacterBuilder
{
    internal static class BundleConfig
    {
        private class BundleConfigOrderer : IBundleOrderer
        {
            IEnumerable<BundleFile> IBundleOrderer.OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var minifyJs = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["MinifyJs"] ?? "true");
            var vendorBundle = new ScriptBundle("~/scripts/vendor")
                .Include("~/assets/js/jquery-1.12.2.min.js")
                .Include("~/assets/js/bootstrap.js")
                .Include("~/assets/js/propeller.js")
                .Include("~/lib/moment/moment.js");


            if (!minifyJs)
            {
                vendorBundle.Transforms.Clear(); //disables minification
            }
            vendorBundle.Orderer = new BundleConfigOrderer();
            bundles.Add(vendorBundle);

            var vendorBundleTwo = new ScriptBundle("~/scripts/vendortwo")
                .Include("~/lib/knockout/knockout-3.4.0.js")
                .Include("~/lib/knockout/knockout-es5.js")
                .Include("~/lib/knockout/knockout.punches.js")
                .Include("~/lib/knockout/ko.plus.js")
                .Include("~/lib/knockout/knockout.reactor.js")
                .Include("~/lib/knockout/knockout.mapping.js")
                .Include("~/lib/fuse.js")
                .Include("~/assets/js/alert.js")
                .Include("~/assets/js/wNumb.js")
                .Include("~/assets/js/nouislider.js");


            if (!minifyJs)
            {
                vendorBundleTwo.Transforms.Clear(); //disables minification
            }
            vendorBundleTwo.Orderer = new BundleConfigOrderer();
            bundles.Add(vendorBundleTwo);

            var styleBundle = new StyleBundle("~/content/css")
                .Include("~/lib/font-awesome/css/font-awesome.css")
                .Include("~/assets/css/bootstrap.css")
                .Include("~/assets/css/propeller.css")
                .Include("~/themes/css/propeller-theme.css")
                .Include("~/assets/css/alert.css")
                .Include("~/assets/css/sidebar.css");

            styleBundle.Orderer = new BundleConfigOrderer();
            bundles.Add(styleBundle);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css"));
        }
    }
}
