using System.Web.Mvc;
using Chimera.Authentication.Views.UserAccounts;
using Xyperico.Base.Exceptions;


namespace Chimera.Authentication.Web.Areas.UserAccounts.Controllers
{
  public class InfoController : Xyperico.Web.Mvc.Controller
  {
    #region Dependencies

    public IUserAccountViewRepository UserAccountViewRepository { get; set; }

    #endregion


    [ChildActionOnly]
    [AllowAnonymous]
    public ActionResult AccountBox()
    {
      try
      {
        UserAccountView u = UserAccountViewRepository.GetByUserName(User.Identity.Name);
        return PartialView("_AccountBox", u);
      }
      catch (MissingResourceException)
      {
        return PartialView("_AccountBox");
      }
    }
  }
}