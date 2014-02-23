//-----------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Web
{
    using System.Web.Http;

    /// <summary>
    /// Web API Config class
    /// </summary>
    public static class WebApiConfig
    {
        #region Public Methods

        /// <summary>
        /// Registers Web API routes
        /// </summary>
        /// <param name="config">Represents a configuration of HttpServer instances.</param>
        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services

            //// Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        #endregion
    }
}