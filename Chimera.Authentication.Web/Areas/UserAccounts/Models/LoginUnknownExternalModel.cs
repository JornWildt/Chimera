using System.ComponentModel.DataAnnotations;
using Xyperico.Base.DataAnnotations;
using System.Web.Mvc;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class LoginUnknownExternalModel : ExternalLoginBaseData
  {
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.Account))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.Account))]
    public string Password { get; set; }

    public string IsRedirect { get; set; }
  }
}