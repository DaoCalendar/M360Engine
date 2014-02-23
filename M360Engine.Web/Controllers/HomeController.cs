//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Home Controller class
    /// </summary>
    public class HomeController : Controller
    {
        #region Actions

        /// <summary>
        /// GET: /Home/Index
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}