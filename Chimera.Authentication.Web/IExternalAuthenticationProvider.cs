using DotNetOpenAuth.AspNet;


namespace Xyperico.Authentication
{
  public interface IExternalAuthenticationProvider
  {
    IAuthenticationClient AuthenticationClient { get; }
  }
}
