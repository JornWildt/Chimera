using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Xyperico.Authentication.Web.Areas.Account.Models;
using Xyperico.Web.Mvc;
using Xyperico.Base.Exceptions;


namespace Xyperico.Authentication.Web.Areas.Account.Controllers
{
  public class ManageController : Xyperico.Web.Mvc.Controller
  {
    [HttpGet]
    [Authorize]
    public ActionResult Show()
    {
      return View();
    }
  }
}
