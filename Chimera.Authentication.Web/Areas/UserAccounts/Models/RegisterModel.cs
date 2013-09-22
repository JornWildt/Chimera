using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Chimera.Authentication.Shared.UserAccounts;
using Xyperico.Base.DataAnnotations;


namespace Chimera.Authentication.Web.Areas.UserAccounts.Models
{
  public class RegisterModel
  {
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [LocalizedDisplayName("User_name", NameResourceType = typeof(_.AuthWeb))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [LocalizedDisplayName("EMail", NameResourceType = typeof(_.AuthWeb))]
    public string EMail { get; set; }

    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [DataType(DataType.Password)]
    [LocalizedDisplayName("Password", NameResourceType = typeof(_.AuthWeb))]
    [PasswordValidation(ErrorMessageResourceName = "InvalidPassword", ErrorMessageResourceType = typeof(_.AuthWeb))]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = "Required_p0", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [Compare("Password", ErrorMessageResourceName = "PasswordAndConfirmationDoesNotMatch", ErrorMessageResourceType = typeof(_.AuthWeb))]
    [LocalizedDisplayName("Confirm_Password", NameResourceType = typeof(_.AuthWeb))]
    public string ConfirmPassword { get; set; }

    public PasswordPolicy PasswordPolicy = new PasswordPolicy
    {
      MinPasswordLength = 5,
      MinNoOfLowerCaseChars = 3,
      MinNoOfUpperCaseChars = 2,
      MaxNoOfAllowedCharacterRepetitions = 3
    };

    public PasswordStrength PasswordStrength = new PasswordStrength
    {
      StrengthCategories = "weak,medium,strong",
      StrengthColours = "red,magenta,green",
      MinPasswordLengthStrength = "5,8,11",
      MinNoOfLowerCaseCharsStrength = "3,5,7",
      MinNoOfUpperCaseCharsStrength = "2,3,4",
      MaxNoOfAllowedCharacterRepetitionsStrength = "3,2,1"
    };
  }
}