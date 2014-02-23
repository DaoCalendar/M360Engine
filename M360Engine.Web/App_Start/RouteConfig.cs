//-----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route Config class
    /// </summary>
    public class RouteConfig
    {
        #region Public Methods

        /// <summary>
        /// Registers MVC navigation routes
        /// </summary>
        /// <param name="routes">Provides a collection of routes for ASP.NET routing.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        #endregion
    }
}