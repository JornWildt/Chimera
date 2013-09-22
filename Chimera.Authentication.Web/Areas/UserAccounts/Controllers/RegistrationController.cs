using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Chimera.Authentication.Web.Areas.UserAccounts.Models;
using Xyperico.Base.Exceptions;
using Xyperico.Web.Mvc;
using Chimera.Authentication.Core.UserAccounts;
using Chimera.Authentication.Views.UserAccounts;


namespace Chimera.Authentication.Web.Areas.UserAccounts.Controllers
{
  public class RegistrationController : Xyperico.Web.Mvc.Controller
  {
    #region Dependencies

    public IUserAccountViewRepository UserAccountViewRepository { get; set; }
    public IUserNameValidator UserNameValidator { get; set; }

    #endregion


    #region Registration

    [AllowAnonymous]
    [PageLayout("Simple")]
    public ActionResult Register()
    {
      if (User.Identity.IsAuthenticated)
        return RedirectToHome();

      RegisterModel model = new RegisterModel();
      return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [PageLayout("Simple")]
    public ActionResult Register(RegisterModel model)
    {
      if (User.Identity.IsAuthenticated)
        return RedirectToHome();

      if (ModelState.IsValid)
      {
        // Attempt to register the user
        try
        {
          WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { EMail = model.EMail });
          WebSecurity.Login(model.UserName, model.Password);
          return Configuration.Settings.RegisterSuccessUrl.Redirect();
        }
        catch (MembershipCreateUserException ex)
        {
          ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
        }
        catch (InvalidUserNameException)
        {
          ModelState.AddModelError("UserName", _.AuthWeb.InvalidUserName);
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }


    [HttpGet]
    [AllowAnonymous]
    public ActionResult CheckUserName(string value) // The name "value" is specified by the common AJAX verifier
    {
      System.Threading.Thread.Sleep(500);
      if (!UserNameValidator.IsValidUserName(value))
        return Json(new { Ok = false, Message = _.AuthWeb.InvalidUserName }, JsonRequestBehavior.AllowGet);

      try
      {
        UserAccountViewRepository.GetByUserName(value);
        return Json(new { Ok = false, Message = _.AuthWeb.UserNameNotAvailable }, JsonRequestBehavior.AllowGet);
      }
      catch (MissingResourceException)
      {
      }

      return Json(new 
      { 
        Ok = true, 
        Message = _.AuthWeb.UserNameAvailable,
        CheckedValue = value
      }, JsonRequestBehavior.AllowGet);
    }


    [HttpGet]
    [AllowAnonymous]
    public ActionResult CheckEMail(string value) // The name "value" is specified by the common AJAX verifier
    {
      System.Threading.Thread.Sleep(500);

      try
      {
        UserAccountViewRepository.GetByEMail(value);
        return Json(new { Ok = false, Message = _.AuthWeb.EMailAlreadyInUse }, JsonRequestBehavior.AllowGet);
      }
      catch (MissingResourceException)
      {
      }

      return Json(new 
      { 
        Ok = true, 
        Message = _.AuthWeb.EMailNotInUse,
        CheckedValue = value
      }, JsonRequestBehavior.AllowGet);
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [PageLayout("Simple")]
    public ActionResult RegisterUnknownExternal(RegisterUnknownExternalModel model)
    {
      string provider = null;
      string providerUserId = null;

      if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
        return RedirectToHome();

      model.ProviderName = provider;
      model.ProviderUserId = providerUserId;
      model.EMail = model.ProviderEMail;

      if (!string.IsNullOrEmpty(model.IsRedirect))
      {
        ModelState.Clear();
        return View(model);
      }

      if (ModelState.IsValid)
      {
        // Attempt to register the user
        try
        {
          OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
          UserAccountView user = UserAccountViewRepository.GetByUserName(model.UserName);
          //if (!string.IsNullOrEmpty(model.Password))
          //  user.ChangePassword(model.Password, Chimera.Authentication.Shared.UserAccounts.Configuration.Settings.GetPasswordPolicy());
          //if (!string.IsNullOrEmpty(model.EMail))
          //  user.ChangeEMail(model.EMail);
          //UserRepository.Update(user);
          OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);
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
        catch (InvalidUserNameException)
        {
          ModelState.AddModelError("UserName", _.AuthWeb.InvalidUserName);
        }
        catch (MembershipCreateUserException ex)
        {
          ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }


    #endregion


    private static string ErrorCodeToString(MembershipCreateStatus createStatus)
    {
      // See http://go.microsoft.com/fwlink/?LinkID=177550 for
      // a full list of status codes.
      switch (createStatus)
      {
        case MembershipCreateStatus.DuplicateUserName:
          return _.AuthWeb.UserNameNotAvailable;

        case MembershipCreateStatus.DuplicateEmail:
          return _.AuthWeb.DuplicateEMail;

        case MembershipCreateStatus.DuplicateProviderUserKey:
          return _.AuthWeb.DuplicateProviderUserKey;

        // The rest are not used by this SimplerMembershipProvider

        case MembershipCreateStatus.InvalidPassword:
          return "The password provided is invalid. Please enter a valid password value.";

        case MembershipCreateStatus.InvalidEmail:
          return "The e-mail address provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidAnswer:
          return "The password retrieval answer provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidQuestion:
          return "The password retrieval question provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidUserName:
          return "The user name provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.ProviderError:
          return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        case MembershipCreateStatus.UserRejected:
          return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        default:
          return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
      }
    }
  }
}
