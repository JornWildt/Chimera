using Chimera.Authentication.Views.Tests.UserAccounts;

namespace Chimera.Authentication.Views.Tests
{
  public class TestHelper : Xyperico.Base.Testing.TestHelper
  {
    protected IUserAccountViewBuilder UserAccountViewBuilder = ObjectContainer.Resolve<IUserAccountViewBuilder>();


    protected override void TearDown()
    {
      base.TestFixtureTearDown();
      UserAccountViewBuilder.DisposeInstances();
    }
  }
}
