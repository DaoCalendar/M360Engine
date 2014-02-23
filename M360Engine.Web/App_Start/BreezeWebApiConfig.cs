//-----------------------------------------------------------------------
// <copyright file="BreezeWebApiConfig.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
[assembly: WebActivator.PreApplicationStartMethod(typeof(M360Engine.Web.BreezeWebApiConfig), "RegisterBreezePreStart")]

namespace M360Engine.Web
{
    using System.Web.Http;

    /// <summary>
    /// Inserts the Breeze Web API controller route at the front of all Web API routes
    /// </summary>
    /// <remarks>
    /// This class is discovered and run during startup; see
    /// http://blogs.msdn.com/b/davidebb/archive/2010/10/11/light-up-your-nupacks-with-startup-code-and-webactivator.aspx
    /// </remarks>
    public static class BreezeWebApiConfig
    {
        #region Public Methods

        /// <summary>
        /// Registers breeze API routes
        /// </summary>
        public static void RegisterBreezePreStart()
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("BreezeApi", "breeze/{controller}/{action}");
        }

        #endregion
    }
}