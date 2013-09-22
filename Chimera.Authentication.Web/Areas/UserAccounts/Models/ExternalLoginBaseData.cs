namespace Chimera.Authentication.Web.Areas.UserAccounts.Models
{
  public class ExternalLoginBaseData
  {
    public string ExternalLoginData { get; set; }

    public string ProviderName { get; set; }

    public string ProviderUserId { get; set; }

    // Display name only - do not use as authenticated data!
    public string ProviderUserName { get; set; }

    // Display e-mail only - do not use as authenticated data!
    public string ProviderEMail { get; set; }

    public string ReturnUrl { get; set; }
  }
}