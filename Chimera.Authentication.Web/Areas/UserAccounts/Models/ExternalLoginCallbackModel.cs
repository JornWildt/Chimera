namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class ExternalLoginCallbackModel : ExternalLoginBaseData
  {
    public string IsRegister { get; set; }
    
    public string IsLogin { get; set; }
  }
}