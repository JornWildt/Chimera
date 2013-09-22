using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Web.ExternalAuthenticationProviders
{
  public class Yahoo : IExternalAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Yahoo(string clientId, string clientSecret)
    {
      AuthenticationClient = new YahooOpenIdClient();
    }
  }
}
