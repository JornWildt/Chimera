using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Web.ExternalAuthenticationProviders
{
  public class Twitter : IExternalAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Twitter(string clientId, string clientSecret)
    {
      AuthenticationClient = new TwitterClient(clientId, clientSecret);
    }
  }
}
