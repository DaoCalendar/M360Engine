//-----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Primal Trigger">
//     Copyright (c) Primal Trigger. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Web
{
    using System.Web.Optimization;

    /// <summary>
    /// Bundling and minification are two techniques you can use in ASP.NET 4.5 to improve request load time.  
    /// Bundling and minification improves load time by reducing the number of requests to the server and reducing the size of requested assets (such as CSS and JavaScript.)
    /// </summary>
    public class BundleConfig
    {
        #region Public Methods

        /// <summary>
        /// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// </summary>
        /// <param name="bundles">Contains and manages the set of registered Bundle objects in an ASP.NET application.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            //// Vendor Scripts
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/jquery-2.1.0.js",
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/toastr.js",
                "~/Scripts/moment.js",
                "~/Scripts/ui-bootstrap-tpls-0.10.0.js",
                "~/Scripts/spin.js",
                "~/Scripts/breeze.debug.js",
                "~/Scripts/breeze.angular.q.js",
                "~/Scripts/breeze.directives.validation.js",
                "~/Scripts/breeze.saveErrorExtensions.js"));

            //// Bootstrapping
            bundles.Add(new ScriptBundle("~/bundles/bootstrapping").Include(
                "~/app/app.js",
                "~/app/config.js",
                "~/app/config.exceptionHandler.js",
                "~/app/config.route.js",
                "~/app/config.breeze.js"));

            //// Common Modules
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/app/common/common.js",
                "~/app/common/logger.js",
                "~/app/common/spinner.js"));

            //// Common bootstrap Modules
            bundles.Add(new ScriptBundle("~/bundles/commonbootstrap").Include(
                "~/app/common/bootstrap/bootstrap.dialog.js"));

            //// App
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/home/home.js",
                "~/app/advancedSearch/advancedSearch.js",
                "~/app/sales/sales.js",
                "~/app/procurement/procurement.js",
                "~/app/customers/customers.js",
                "~/app/tickets/tickets.js",
                "~/app/knowledgebase/knowledgebase.js",
                "~/app/bestPractices/bestPractices.js",
                "~/app/humanResources/humanResources.js",
                "~/app/finance/finance.js",
                "~/app/management/management.js",
                "~/app/layout/shell.js",
                "~/app/layout/sidebar.js"));

            //// App Services
            bundles.Add(new ScriptBundle("~/bundles/appservices").Include(
                "~/app/services/datacontext.js",
                "~/app/services/directives.js",
                "~/app/services/entityManagerFactory.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            //// Styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/ie10mobile.css",
                "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/toastr.css",
                "~/Content/customtheme.css",
                "~/Content/styles.css",
                "~/Content/breeze.directives.css"));
        }

        #endregion
    }
}