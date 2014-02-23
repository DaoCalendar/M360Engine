//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(M360Engine.Web.Startup))]

namespace M360Engine.Web
{
    /// <summary>
    /// Startup class
    /// </summary>
    public partial class Startup
    {
        #region Public Methods

        /// <summary>
        /// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// </summary>
        /// <param name="app">
        /// In Visual Basic and C#, you can call this method as an instance method on any object of type IAppBuilder. 
        /// When you use instance method syntax to call this method, omit the first parameter. 
        /// For more information, see http://msdn.microsoft.com/en-us/library/bb384936(v=vs.100).aspx or http://msdn.microsoft.com/en-us/library/bb383977(v=vs.100).aspx.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        #endregion
    }
}