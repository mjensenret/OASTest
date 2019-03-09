using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace OASTest {

    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");

            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-3.3.1.js");

            // Bootstrap
            scriptBundle
                .Include("~/Scripts/bootstrap.js");

            //OAS Scripts
            scriptBundle
                .IncludeDirectory("~/Scripts/OAS/", "opc-lib-min.js");
            //scriptBundle
            //    .IncludeDirectory("~/Scripts/OAS/flot/", "*.js");



            // Bootstrap
            styleBundle
                .Include("~/Content/bootstrap.css");

            // Custom site styles
            styleBundle
                .Include("~/Content/Site.css");

            //OAS styles
            styleBundle
                .Include("~/Content/jquery.dataTables.css");
            styleBundle
                .Include("~/Content/jquery.dataTables_themeroller.css");
            styleBundle
                .Include("~/Content/font-awesome.min.css");
            styleBundle
                .Include("~/Content/opc-alarm-style.css");
            styleBundle
                .Include("~/Content/opc-modal.css");
            styleBundle
                .Include("~/Content/opc-style.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}