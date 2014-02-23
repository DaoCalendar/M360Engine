//-----------------------------------------------------------------------
// <copyright file="HomeControllerTest.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Test.Controllers
{
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Web.Controllers;

    /// <summary>
    /// Home Controller Test class
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        #region Test Methods

        /// <summary>
        /// Checks if Index View is returned
        /// </summary>
        [TestMethod]
        public void Index()
        {
            //// Arrange
            var controller = new HomeController();

            //// Act
            var result = controller.Index() as ViewResult;

            //// Assert
            Assert.IsNotNull(result);
        }

        #endregion
    }
}