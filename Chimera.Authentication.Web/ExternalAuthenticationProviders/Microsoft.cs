using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Web.ExternalAuthenticationProviders
{
  public class Microsoft : IExternalAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Microsoft(string clientId, string clientSecret)
    {
      AuthenticationClient = new MicrosoftClient(clientId, clientSecret);
    }
  }
}
