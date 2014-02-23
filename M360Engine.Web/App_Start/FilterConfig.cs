//-----------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Web
{
    using System.Web.Mvc;

    /// <summary>
    /// Filter Config class
    /// </summary>
    public class FilterConfig
    {
        #region Public Methods

        /// <summary>
        /// Global filters run for every action of every controller. 
        /// You can register a global filter using the Filters static registration endpoint.
        /// </summary>
        /// <param name="filters">Represents a class that contains all the global filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        #endregion
    }
}