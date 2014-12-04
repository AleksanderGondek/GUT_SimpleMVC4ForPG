using System;
using System.Web.Mvc;
using System.Web.Security;
using SimpleMVC4.Context;
using SimpleMVC4.Helpers;
using SimpleMVC4.Models.Accounts;
using SimpleMVC4.Repositories;
using WebMatrix.WebData;

namespace SimpleMVC4.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountsRepository _accountsRepository;

        public AccountController(ISimpleMvc4Context databaseContext)
        {
            _accountsRepository = new AccountsRepository(databaseContext);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                WebSecurity.Login(model.UserName, model.Password);
                return RedirectToAction("Index", "Home");
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", AccountControllerHelper.ErrorCodeToString(e.StatusCode));
            }

            return View(model);
        }

        public ActionResult Manage(AccountControllerHelper.ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == AccountControllerHelper.ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == AccountControllerHelper.ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == AccountControllerHelper.ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";

            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ChangePasswordModel model)
        {
            if (!ModelState.IsValid) return View(model);

            ViewBag.ReturnUrl = Url.Action("Manage");
            
            bool changePasswordSucceeded;
            try
            {
                changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }

            if (changePasswordSucceeded)
            {
                return RedirectToAction("Manage", new { Message = AccountControllerHelper.ManageMessageId.ChangePasswordSuccess });
            }
                    
            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
