using Chimera.Authentication.Core.UserAccounts;


namespace Chimera.Authentication.Core.Tests
{
  public class TestHelper : Xyperico.Base.Testing.TestHelper
  {
    protected IUserNameValidator UserNameValidator = new FilebasedUserNameValidator();
  }
}
