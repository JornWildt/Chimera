using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Web.ExternalAuthenticationProviders
{
  public class Facebook : IExternalAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Facebook(string clientId, string clientSecret)
    {
      AuthenticationClient = new FacebookClient(clientId, clientSecret);
    }
  }
}
