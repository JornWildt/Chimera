using Chimera.Authentication.Views.UserAccounts;
using Xyperico.Base.Testing;


namespace Chimera.Authentication.Views.Tests.UserAccounts
{
  public interface IUserAccountViewBuilder : IDisposingBuilder<UserAccountView>
  {
    UserAccountView BuildUserAccountView(string userName = null, string email = null);
  }
}
