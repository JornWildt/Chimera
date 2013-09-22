using System.ComponentModel.DataAnnotations;
using Xyperico.Base.DataAnnotations;
using System.Web.Mvc;


namespace Chimera.Authentication.Web.Areas.UserAccounts.Models
{
  public class RegisterUnknownExternalModel : ExternalLoginBaseData
  {
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.AuthWeb))]
    public string UserName { get; set; }

    [LocalizedDisplayName("EMail", NameResourceType = typeof(_.AuthWeb))]
    public string EMail { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "p0_MustBe_p2_CharsLong", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.AuthWeb))]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessageResourceName = "PasswordAndConfirmationDoesNotMatch", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [LocalizedDisplayName("Confirm_Password", NameResourceType = typeof(_.AuthWeb))]
    public string ConfirmPassword { get; set; }

    public string IsRedirect { get; set; }
  }
}