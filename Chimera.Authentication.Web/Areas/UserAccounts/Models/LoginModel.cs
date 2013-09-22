using System.ComponentModel.DataAnnotations;
using Xyperico.Base.DataAnnotations;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class LoginModel
  {
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.Account))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.Account))]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.Account))]
    public string Password { get; set; }

    [LocalizedDisplayName("Remember_me", NameResourceType=typeof(_.Account))]
    public bool RememberMe { get; set; }
  }
}