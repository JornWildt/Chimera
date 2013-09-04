using Chimera.Authentication.Core.UserAccounts;
using NUnit.Framework;
using Xyperico.Base;


namespace Chimera.Authentication.Core.Tests
{
  [SetUpFixture]
  public class SetupFixture
  {
    public static void Setup(IObjectContainer container)
    {
      container.AddComponent<IUserNameValidator, FilebasedUserNameValidator>();
    }


    [SetUp]
    public void TestSetup()
    {
      Xyperico.Base.Testing.TestHelper.ClearObjectContainer();
      Setup(Xyperico.Base.Testing.TestHelper.ObjectContainer);
    }
  }
}
