//-----------------------------------------------------------------------
// <copyright file="Startup.Auth.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Web
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;

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
        public void ConfigureAuth(IAppBuilder app)
        {
            //// Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            //// Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //// Uncomment the following lines to enable logging in with third party login providers
            ////app.UseMicrosoftAccountAuthentication(
            ////    clientId: "",
            ////    clientSecret: "");

            ////app.UseTwitterAuthentication(
            ////   consumerKey: "",
            ////   consumerSecret: "");

            ////app.UseFacebookAuthentication(
            ////   appId: "",
            ////   appSecret: "");

            ////app.UseGoogleAuthentication();
        }

        #endregion
    }
}