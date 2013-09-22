using System.Web.Mvc;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Xyperico.Authentication.Web.Areas.Account.Models;
using WebMatrix.WebData;
using Xyperico.Web.Mvc;
using Xyperico.Base.Exceptions;
using System.Web.Security;


namespace Xyperico.Authentication.Web.Areas.Account.Controllers
{
  public class LoginController : Xyperico.Web.Mvc.Controller
  {
    #region Dependencies

    public IUserRepository UserRepository { get; set; }

    #endregion


    #region Standard login

    [HttpGet]
    [PageLayout("Simple")]
    public ActionResult show(string returnUrl)
    {
      if (User.Identity.IsAuthenticated)
        return RedirectToHome();

      ViewBag.ReturnUrl = returnUrl;
      return View();
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [PageLayout("Simple")]
    public ActionResult show(LoginModel model, string returnUrl)
    {
      if (User.Identity.IsAuthenticated)
        return RedirectToHome();

      if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
      {
        if (string.IsNullOrEmpty(returnUrl))
          return Configuration.Settings.LoginSuccessUrl.Redirect();
        else
          return Redirect(returnUrl);
      }

      // If we got this far, something failed, redisplay form
      ModelState.AddModelError("", _.Account.WrongPassword);
      return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult logout()
    {
      WebSecurity.Logout();

      return RedirectToHome();
    }

    #endregion


    #region External logins (OpenID, Facebook etc.)

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
    }


    [HttpGet]
    [AllowAnonymous]
    [PageLayout("Simple")]
    public ActionResult ExternalLoginCallback(string returnUrl)
    {
      AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
      if (!result.IsSuccessful)
      {
        return RedirectToAction("ExternalLoginFailure");
      }

      if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
      {
        return RedirectToLocal(returnUrl);
      }

      if (User.Identity.IsAuthenticated)
      {
        // If the current user is logged in add the new account
        OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
        return RedirectToLocal(returnUrl);
      }
      else
      {
        // User is new, redirect to external registration chooser (login-existing or register)
        string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
        ExternalLoginCallbackModel model = new ExternalLoginCallbackModel
        {
          ProviderName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName,
          ExternalLoginData = loginData,
          ProviderUserId = result.ProviderUserId,
          ProviderUserName = result.UserName,
          ProviderEMail = result.UserName,
          ReturnUrl = returnUrl
        };
        return View(model);
      }
    }


    [HttpPost]
    [AllowAnonymous]
    [PageLayout("Simple")]
    public ActionResult LoginUnknownExternal(LoginUnknownExternalModel model)
    {
      string provider = null;
      string providerUserId = null;

      if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
        return RedirectToHome();

      model.ProviderName = provider;
      model.ProviderUserId = providerUserId;

      if (!string.IsNullOrEmpty(model.IsRedirect))
      {
        ModelState.Clear();
        return View(model);
      }

      if (ModelState.IsValid)
      {
        if (WebSecurity.Login(model.UserName, model.Password))
        {
          try
          {
            OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
            return Configuration.Settings.RegisterSuccessUrl.Redirect();
          }
          catch (DuplicateKeyException ex)
          {
            if (ex.Key == "UserName")
              ModelState.AddModelError("", "User name is already in use");
            else if (ex.Key == "EMail")
              ModelState.AddModelError("", "EMail is already in use");
            else if (ex.Key == "ExternalLogin")
              ModelState.AddModelError("", "External login is already in use");
            else
              ModelState.AddModelError("", "Unknown error");
          }
          catch (MembershipCreateUserException ex)
          {
            ModelState.AddModelError("", ex.Message);
          }
        }
        else
          ModelState.AddModelError("", _.Account.WrongPassword);
      }

      return View(model);
    }


    [ChildActionOnly]
    [AllowAnonymous]
    public ActionResult ExternalLoginList(string returnUrl, string header)
    {
      ViewBag.ReturnUrl = returnUrl;
      ExternalLoginListModel model = new ExternalLoginListModel
      {
        Header = header,
        Clients = OAuthWebSecurity.RegisteredClientData
      };
      return PartialView("_ExternalLoginList", model);
    }

    #endregion


    #region Helpers

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToHome();
      }
    }

    #endregion
  }
}
