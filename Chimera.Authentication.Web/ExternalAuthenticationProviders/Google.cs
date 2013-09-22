using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Web.ExternalAuthenticationProviders
{
  public class Google : IExternalAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Google(string clientId, string clientSecret)
    {
      AuthenticationClient = new GoogleOpenIdClient();
    }
  }
}
