using System.Collections.Generic;
using Microsoft.Web.WebPages.OAuth;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  public class ExternalLoginListModel
  {
    public string Header { get; set; }
    public ICollection<AuthenticationClientData> Clients { get; set; }
  }
}