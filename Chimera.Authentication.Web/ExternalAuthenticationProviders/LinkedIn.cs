using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Web.ExternalAuthenticationProviders
{
  public class LinkedIn : IExternalAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public LinkedIn(string clientId, string clientSecret)
    {
      AuthenticationClient = new LinkedInClient(clientId, clientSecret);
    }
  }
}
