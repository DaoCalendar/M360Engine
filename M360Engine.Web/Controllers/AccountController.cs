//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Model;

    /// <summary>
    /// Account Controller class
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        #region Private Members

        /// <summary>
        /// Used for XSRF protection when adding external logins 
        /// </summary>
        private const string XsrfKey = "XsrfId";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userManager">Selected user manager</param>
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Manage Messages
        /// </summary>
        public enum ManageMessageId
        {
            /// <summary>
            /// Change Password Success
            /// </summary>
            ChangePasswordSuccess,

            /// <summary>
            /// Set Password Success
            /// </summary>
            SetPasswordSuccess,

            /// <summary>
            /// Remove Login Success
            /// </summary>
            RemoveLoginSuccess,

            /// <summary>
            /// Error Message
            /// </summary>
            Error
        }

        /// <summary>
        /// Gets UserManager
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; private set; }

        /// <summary>
        /// Gets a list of objects to validate in the authentication manager.
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// GET: /Account/Login 
        /// </summary>
        /// <param name="returnUrl">URL to return to</param>
        /// <returns>Login View</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        /// <summary>
        /// POST: /Account/Login 
        /// </summary>
        /// <param name="model">Selected login model</param>
        /// <param name="returnUrl">URL to return to</param>
        /// <returns>User logged in</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindAsync(model.UserName, model.Password);

            if (user != null)
            {
                await SignInAsync(user, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// GET: /Account/Register 
        /// </summary>
        /// <returns>Register View</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST: /Account/Register 
        /// </summary>
        /// <param name="model">Selected register model</param>
        /// <returns>New user registered</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser { UserName = model.UserName };
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// POST: /Account/Disassociate 
        /// </summary>
        /// <param name="loginProvider">Selected login provider</param>
        /// <param name="providerKey">Selected provider key</param>
        /// <returns>User disassociated from external login provider</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            ManageMessageId? message = result.Succeeded ? ManageMessageId.RemoveLoginSuccess : ManageMessageId.Error;

            return RedirectToAction("Manage", new { Message = message });
        }

        /// <summary>
        /// GET: /Account/Manage 
        /// </summary>
        /// <param name="message">Selected message</param>
        /// <returns>Message shown</returns>
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : string.Empty;
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");

            return View();
        }

        /// <summary>
        /// POST: /Account/Manage
        /// </summary>
        /// <param name="model">Selected manager user model</param>
        /// <returns>User associated to external login provider</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            var hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (hasPassword)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                }

                AddErrors(result);
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                var state = ModelState["OldPassword"];

                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// POST: /Account/ExternalLogin
        /// </summary>
        /// <param name="provider">Selected provider</param>
        /// <param name="returnUrl">URL to return to</param>
        /// <returns>External login results</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        /// <summary>
        /// GET: /Account/ExternalLoginCallback
        /// </summary>
        /// <param name="returnUrl">URL to return to</param>
        /// <returns>User logged in via external provider</returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);

            if (user != null)
            {
                await SignInAsync(user, false);

                return RedirectToLocal(returnUrl);
            }

            // If the user does not have an account, then prompt the user to create an account
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
        }

        /// <summary>
        /// POST: /Account/LinkLogin
        /// </summary>
        /// <param name="provider">Selected provider</param>
        /// <returns>External login provider link</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        /// <summary>
        /// GET: /Account/LinkLoginCallback
        /// </summary>
        /// <returns>External login provider link</returns>
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());

            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }

            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);

            return result.Succeeded ? RedirectToAction("Manage") : RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        /// <summary>
        /// POST: /Account/ExternalLoginConfirmation
        /// </summary>
        /// <param name="model">Selected external login confirmation model</param>
        /// <param name="returnUrl">URL to return to</param>
        /// <returns>External login confirmation</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var user = new ApplicationUser { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);

                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }

        /// <summary>
        /// POST: /Account/LogOff
        /// </summary>
        /// <returns>User logged off</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// GET: /Account/ExternalLoginFailure
        /// </summary>
        /// <returns>External Login Failure View</returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        /// <summary>
        /// Removes selected account from list
        /// </summary>
        /// <returns>Remove Account Partial View</returns>
        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;

            return PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Disposes of the resources (other than memory) used by the Form.
        /// This method is called by the public Dispose method and the Finalize method. 
        /// This method invokes the Dispose method of each referenced object.
        /// Dispose will be called automatically if the form is shown using the Show method. 
        /// If another method such as ShowDialog is used, or the form is never shown at all, you must call Dispose yourself within your application.
        /// </summary>
        /// <param name="disposing">
        /// Dispose invokes the protected Dispose(Boolean) method with the disposing parameter set to true. 
        /// Finalize invokes Dispose with disposing set to false.
        /// When the disposing parameter is true, this method releases all resources held by any managed objects that this Form references. 
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Signs in the selected user
        /// </summary>
        /// <param name="user">Selected user</param>
        /// <param name="isPersistent">If user must be persisted or not</param>
        /// <returns>User signed in</returns>
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        /// <summary>
        /// Adds error to be shown
        /// </summary>
        /// <param name="result">Errors added to Model State</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        /// <summary>
        /// Check if user has password
        /// </summary>
        /// <returns>True if user has password</returns>
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        /// <summary>
        /// Redirects user to local URL
        /// </summary>
        /// <param name="returnUrl">URL to return to</param>
        /// <returns>User redirected to local URL</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Challenge Result class
        /// </summary>
        private class ChallengeResult : HttpUnauthorizedResult
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ChallengeResult" /> class.
            /// </summary>
            /// <param name="provider">Selected provider</param>
            /// <param name="redirectUri">URI to be redirected to</param>
            /// <param name="userId">Selected User Id</param>
            public ChallengeResult(string provider, string redirectUri, string userId = null)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            /// <summary>
            /// Gets or sets LoginProvider
            /// </summary>
            private string LoginProvider { get; set; }

            /// <summary>
            /// Gets or sets RedirectUri
            /// </summary>
            private string RedirectUri { get; set; }

            /// <summary>
            /// Gets or sets UserId
            /// </summary>
            private string UserId { get; set; }

            #region Public Methods

            /// <summary>
            /// Enables processing of the result of an action method by a custom type that inherits from the ActionResult class.
            /// </summary>
            /// <param name="context">Encapsulates information about an HTTP request that matches specified RouteBase and ControllerBase instances.</param>
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };

                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }

            #endregion
        }

        #endregion
    }
}